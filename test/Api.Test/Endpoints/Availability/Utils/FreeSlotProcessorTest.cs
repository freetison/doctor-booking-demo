using Api.Endpoints.Availability.Models.Domain;
using Api.Endpoints.Availability.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;

namespace Api.Test.Endpoints.Availability.Utils;

[TestFixture]
public class FreeSlotProcessorTests
{
    private WeekDaySchedule? _weekDaySchedule;
    private List<(string Day, List<FreeSlot>)> _freeSlots;
    private string _inputDate;

    [SetUp]
    public void Setup()
    {
        var json = @"{
            ""Days"": {
                ""Friday"": {
                    ""BusySlots"": [
                        {
                            ""End"": ""2024-08-02T11:20:00"",
                            ""Start"": ""2024-08-02T11:10:00""
                        },
                        {
                            ""End"": ""2024-08-02T08:10:00"",
                            ""Start"": ""2024-08-02T08:00:00""
                        }
                    ],
                    ""WorkPeriod"": {
                        ""EndHour"": 16,
                        ""LunchEndHour"": 14,
                        ""LunchStartHour"": 13,
                        ""StartHour"": 8
                    }
                },
                ""Monday"": {
                    ""BusySlots"": [
                        {
                            ""End"": ""2024-07-29T09:10:00"",
                            ""Start"": ""2024-07-29T09:00:00""
                        },
                        {
                            ""End"": ""2024-07-29T09:20:00"",
                            ""Start"": ""2024-07-29T09:10:00""
                        }
                    ],
                    ""WorkPeriod"": {
                        ""EndHour"": 17,
                        ""LunchEndHour"": 14,
                        ""LunchStartHour"": 13,
                        ""StartHour"": 9
                    }
                },
                ""Wednesday"": {
                    ""BusySlots"": [
                    ],
                    ""WorkPeriod"": {
                        ""EndHour"": 17,
                        ""LunchEndHour"": 14,
                        ""LunchStartHour"": 13,
                        ""StartHour"": 9
                    }
                }
            },
            ""Facility"": {
                ""Address"": ""Plaza de la independencia 36, 38006 Santa Cruz de Tenerife"",
                ""FacilityId"": ""e88be02d-56a8-420e-be99-eca522763221"",
                ""Name"": ""Las Palmeras""
            },
            ""SlotDurationMinutes"": 10
        }";

        _weekDaySchedule = JsonConvert.DeserializeObject<WeekDaySchedule>(json);
        _inputDate = "20240729"; 
        _freeSlots = new FreeSlotProcessor().FindFreeSlots(_weekDaySchedule, _inputDate);
    }

    [Test]
    public void FindFreeSlots_ShouldReturnCorrectFreeSlotsForWednesday()
    {
        // Assert
        var wednesdaySlots = _freeSlots.Find(x => x.Day == "Wednesday").Item2;
        wednesdaySlots.Count.ShouldBe(42);
    }

    [Test]
    public void FindFreeSlots_ShouldHandleBusySlotsOnMonday()
    {
        // Assert
        var tuesdaySlots = _freeSlots.Find(x => x.Day == "Monday").Item2;
        tuesdaySlots.Count.ShouldBe(40); 

    }

    [Test]
    public void FindFreeSlots_ShouldHandleBusySlotsOnFriday()
    {
        // Assert
        var fridaySlots = _freeSlots.Find(x => x.Day == "Friday").Item2;
        fridaySlots.Count.ShouldBe(42); 

    }
}