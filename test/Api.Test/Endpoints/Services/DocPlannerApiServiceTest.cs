using System.Net;
using Api.Endpoints.Appointments.Domain;
using Api.Endpoints.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RestSharp;
using Shouldly;

namespace Api.Test.Endpoints.Services;

[TestFixture]
public class DocPlannerApiServiceTests
{
    private Mock<IRestClient> _restClientMock;
    private Mock<ILogger<DocPlannerApiService>> _loggerMock;
    private DocPlannerApiService _service;
    private CancellationToken _cancellationToken;

    [SetUp]
    public void Setup()
    {
        _restClientMock = new Mock<IRestClient>();
        _loggerMock = new Mock<ILogger<DocPlannerApiService>>();
        _service = new DocPlannerApiService(_restClientMock.Object, _loggerMock.Object);
        _cancellationToken = new CancellationToken();
    }

    [Test]
    public async Task GetWeeklyAvailability_ShouldReturnContent_WhenRequestIsSuccessful()
    {
        // Arrange
        var date = "2023-07-01";
        var expectedContent = "expected content";
        var response = new RestResponse
        {
            StatusCode = HttpStatusCode.OK,
            Content = expectedContent
        };

        _restClientMock
            .Setup(client =>
                client.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Resource == $"/api/availability/GetWeeklyAvailability/{date}"),
                    _cancellationToken))
            .ReturnsAsync(response);

        // Act
        var result = await _service.GetWeeklyAvailability(date, _cancellationToken);

        // Assert
        result.ShouldBe(expectedContent);
    }

    [Test]
    public void GetWeeklyAvailability_ShouldThrowException_WhenRequestFails()
    {
        // Arrange
        var date = "2023-07-01";
        var response = new RestResponse
        {
            StatusCode = HttpStatusCode.BadRequest
        };

        _restClientMock
            .Setup(client => client.ExecuteAsync(It.Is<RestRequest>(r => r.Resource == $"/api/availability/GetWeeklyAvailability/{date}"), _cancellationToken))
            .ReturnsAsync(response);

        // Act & Assert
        var exception = Should.Throw<Exception>(async () => await _service.GetWeeklyAvailability(date, _cancellationToken));
        exception.Message.ShouldBe($"Request failed with status code {response.StatusCode}");

        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Request failed with status code {response.StatusCode}")),
                It.IsAny<Exception>(),
                ((Func<It.IsAnyType, Exception, string>)It.IsAny<object>())!));
    }
    [Test]
    public Task TakeSlot_ShouldThrowException_WhenRequestFails()
    {
        // Arrange
        var appointment = new Appointment
        {
            Patient = null
        }; 
        var response = new RestResponse
        {
            StatusCode = HttpStatusCode.BadRequest
        };

        _restClientMock
            .Setup(client =>
                client.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Resource == "/api/availability/TakeSlot" && r.Method == Method.Post),
                    _cancellationToken))
            .ReturnsAsync(response);

        // Act & Assert
        Should.Throw<Exception>(async () => await _service.TakeSlot(appointment, _cancellationToken));
        return Task.CompletedTask;
    }

    [Test]
    public async Task TakeSlot_ShouldSucceed_WhenRequestIsSuccessful()
    {
        // Arrange
        var appointment = new Appointment
        {
            Patient = null
        }; 
        var response = new RestResponse
        {
            StatusCode = HttpStatusCode.OK
        };

        _restClientMock
            .Setup(client =>
                client.ExecuteAsync(
                    It.Is<RestRequest>(r => r.Resource == "/api/availability/TakeSlot" && r.Method == Method.Post),
                    _cancellationToken))
            .ReturnsAsync(response);

        // Act
        await _service.TakeSlot(appointment, _cancellationToken);

        // Assert
        // No exception means the test passed
    }
}