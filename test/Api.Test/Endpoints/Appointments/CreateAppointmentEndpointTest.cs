using System.Net;
using System.Text;
using Api.Endpoints.Appointments;
using Api.Endpoints.Appointments.Domain;
using Api.Endpoints.Services;
using FakeItEasy;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;

namespace Api.Test.Endpoints.Appointments;

[TestFixture]
public class CreateAppointmentEndpointTest
{
    private Appointment? _request;
    private IDocPlannerApiService _fakeDocPlannerApiService;
    private CreateAppointmentEndpoint _endpoint; 
        
    [SetUp]
    public void SetUp()
    {
        // Arrange
        string appointmentJson = @"{
                ""Comments"": ""Routine checkup"",
                ""End"": ""2024-08-12T07:20:00.000Z"",
                ""FacilityId"": ""e88be02d-56a8-420e-be99-eca522763221"",
                ""Patient"": {
                    ""Email"": ""pp@gmail.com"",
                    ""Name"": ""Pedro"",
                    ""Phone"": 12345687,
                    ""SecondName"": ""Perez""
                },
                ""Start"": ""2024-08-12T07:10:00.000Z""
            }";
        _request = JsonConvert.DeserializeObject<Appointment>(appointmentJson);
        
        _fakeDocPlannerApiService = A.Fake<IDocPlannerApiService>();
        _endpoint = Factory.Create<CreateAppointmentEndpoint>(
            ctx =>
            {
                ctx.AddTestServices(
                    s =>
                    {
                        s.AddTransient<IDocPlannerApiService>(_ => _fakeDocPlannerApiService);
                    });
            });
    }
    
    [Test]
    public async Task Configure_Should_Setup_Endpoint_Correctly()
    {
        var expected = "In process";
        
        // Act
        await _endpoint.HandleAsync(_request, default);
    
        // Asserts
        _endpoint.ValidationFailed.ShouldBe(false);
        _endpoint.Response.ShouldBe(expected);
        _endpoint.HttpContext.Response.StatusCode.ShouldBe(StatusCodes.Status200OK);
    }
    
    
    
    
    
}