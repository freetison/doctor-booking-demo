using Api.Common.Exceptions;
using Api.Endpoints.Availability;
using Api.Endpoints.Availability.Events;
using Api.Endpoints.Availability.Models;
using Api.Endpoints.Availability.Models.Domain;
using Api.Endpoints.Availability.Utils;
using Api.Endpoints.Services;
using FakeItEasy;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Language.Flow;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;
using Times = Moq.Times;

namespace Api.Test.Endpoints.Availability
{
    public class GetWeeklyAvailabilityEndpointTests : Endpoint<WeeklyAvailabilityRequest, FreeWeekSchedule>
    {
        private readonly Mock<IDocPlannerApiService> _docPlannerApiServiceMock = new();
        private readonly Mock<IFreeSlotProcessor> _freeSlotProcessorMock = new();
        private IReturnsResult<GetWeeklyAvailabilityEndpoint> _endpointMock;
        
        private readonly string _mockResult = "{\"Json\":\"Test\"}";
        private readonly WeeklyAvailabilityRequest _request = new() { Date = "20240729" };
        private GetWeeklyAvailabilityEndpoint _endpoint;
        private readonly CancellationToken _cancellationToken = new();
        private WeeklySchedule _weeklySchedule;
        private WeekDaySchedule? _weekDaySchedule;
        
        [SetUp]
        public Task SetUp()
        {
            string weekDayScheduleJson = @"{
            'Days': {
                'Friday': {
                    'BusySlots': [
                        {
                            'End': '2024-08-02T11:20:00',
                            'Start': '2024-08-02T11:10:00'
                        },
                    ],
                    'WorkPeriod': {
                        'EndHour': 16,
                        'LunchEndHour': 14,
                        'LunchStartHour': 13,
                        'StartHour': 8
                    }
                }
            },
            'Facility': {
                'Address': 'Plaza de la independencia 36, 38006 Santa Cruz de Tenerife',
                'FacilityId': 'e88be02d-56a8-420e-be99-eca522763221',
                'Name': 'Las Palmeras'
            },
            'SlotDurationMinutes': 10
            }";
            _weekDaySchedule = JsonConvert.DeserializeObject<WeekDaySchedule>(weekDayScheduleJson);
    
            return Task.CompletedTask;
        }
 
        [Test, Order(1) ]
        public async Task ShouldThrowNotDataToProceedException_WhenWeeklyAvailabilityExist_But_NoFreeSlots()
        {
            // Arrange
            _docPlannerApiServiceMock
                .Setup(x => x.GetWeeklyAvailability(_request.Date, _cancellationToken))
                .ReturnsAsync(_mockResult);

            _freeSlotProcessorMock
                .Setup(x => x.FindFreeSlots(It.IsAny<WeekDaySchedule>(), _request.Date))
                .Returns(new List<(string Day, List<FreeSlot>)>());
            
            _endpoint = new GetWeeklyAvailabilityEndpoint(
                _docPlannerApiServiceMock.Object,
                _freeSlotProcessorMock.Object
            );
            
            // Act && Assert
            await Should.ThrowAsync<NotDataToProceedException>(async ( ) => await  _endpoint.ExecuteAsync(_request, _cancellationToken));
        }
        
        [Test, Order(2) ]
        public async Task ShouldThrowNotFoundException_WhenWeeklyAvailabilityDoesNotExist()
        {
            // Arrange
            _docPlannerApiServiceMock
                .Setup(x => x.GetWeeklyAvailability(_request.Date, _cancellationToken))
                .ReturnsAsync((string?)null);

            _endpoint = new GetWeeklyAvailabilityEndpoint(
                _docPlannerApiServiceMock.Object,
                _freeSlotProcessorMock.Object
            );
            
            // Act && Assert
            await Should.ThrowAsync<NotFoundException>(async () => await _endpoint.ExecuteAsync(_request, _cancellationToken));
        }

        [Test, Order(3) ]
        public async Task ShouldReturnFreeWeekSchedule_WhenWeeklyAvailabilityExists()
        {
            // Arrange
            _docPlannerApiServiceMock
                .Setup(x => x.GetWeeklyAvailability(_request.Date, _cancellationToken))
                .ReturnsAsync(_mockResult);

            var freeSlots = new List<(string Day, List<FreeSlot>)>
            {
                ("Friday", [
                    new()
                    {
                        Start = DateTime.Now,
                        End = DateTime.Today
                    }
                ])
            };

            _freeSlotProcessorMock
                .Setup(x => x.FindFreeSlots(It.IsAny<WeekDaySchedule>(), _request.Date))
                .Returns(freeSlots);
            
            _freeSlotProcessorMock
                .Setup(x => x.ToWeekDaySchedule(_mockResult))
                .Returns(_weekDaySchedule);
            
            var fakeEventHandler = A.Fake<IEventHandler<AvailabilityProcessedEvent>>();
            
            Factory.RegisterTestServices(s =>
            {
                s.AddSingleton(fakeEventHandler);
            });
           
            _endpoint = new GetWeeklyAvailabilityEndpoint(
                _docPlannerApiServiceMock.Object,
                _freeSlotProcessorMock.Object
            );
            
            await _endpoint.ExecuteAsync(_request, _cancellationToken);

            // Asserts
            _endpoint.Response.ShouldBeAssignableTo<FreeWeekSchedule>();
            _freeSlotProcessorMock.Verify(x => x.FindFreeSlots(It.IsAny<WeekDaySchedule>(), _request.Date), Times.Once);
            
        }

        
    }
}
