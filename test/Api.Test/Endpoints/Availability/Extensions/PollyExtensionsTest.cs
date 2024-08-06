using System.Net;
using Api.Endpoints.Availability.Extensions;
using NUnit.Framework;
using RestSharp;
using Shouldly;

namespace Api.Test.Endpoints.Availability.Extensions;

[TestFixture]
public class PollyExtensionsTests
{
    [Test]
    public async Task HttpPolicy_ShouldRetryOnUnauthorized()
    {
        // Arrange
        var unauthorizedResponse = new RestResponse { StatusCode = HttpStatusCode.Unauthorized };
        var retryPolicy = PollyExtensions.HttpPolicy();

        var retryCounter = 0;
        Func<Task<RestResponse>> action = () =>
        {
            retryCounter++;
            if (retryCounter <= 5)
            {
                return Task.FromResult(unauthorizedResponse);
            }
            return Task.FromResult(new RestResponse { StatusCode = HttpStatusCode.OK });
        };

        // Act
        var result = await retryPolicy.ExecuteAsync(action);

        // Assert
        retryCounter.ShouldBe(6); 
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task HttpPolicy_ShouldRetryOnNotFound()
    {
        // Arrange
        var notFoundResponse = new RestResponse { StatusCode = HttpStatusCode.NotFound };
        var retryPolicy = PollyExtensions.HttpPolicy();

        var retryCounter = 0;
        Func<Task<RestResponse>> action = () =>
        {
            retryCounter++;
            if (retryCounter <= 5)
            {
                return Task.FromResult(notFoundResponse);
            }
            return Task.FromResult(new RestResponse { StatusCode = HttpStatusCode.OK });
        };

        // Act
        var result = await retryPolicy.ExecuteAsync(action);

        // Assert
        retryCounter.ShouldBe(6); 
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task HttpPolicy_ShouldRetryOnRequestTimeout()
    {
        // Arrange
        var requestTimeoutResponse = new RestResponse { StatusCode = HttpStatusCode.RequestTimeout };
        var retryPolicy = PollyExtensions.HttpPolicy();

        var retryCounter = 0;
        Func<Task<RestResponse>> action = () =>
        {
            retryCounter++;
            if (retryCounter <= 5)
            {
                return Task.FromResult(requestTimeoutResponse);
            }
            return Task.FromResult(new RestResponse { StatusCode = HttpStatusCode.OK });
        };

        // Act
        var result = await retryPolicy.ExecuteAsync(action);

        // Assert
        retryCounter.ShouldBe(6); 
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task HttpPolicy_ShouldNotRetryOnSuccess()
    {
        // Arrange
        var successResponse = new RestResponse { StatusCode = HttpStatusCode.OK };
        var retryPolicy = PollyExtensions.HttpPolicy();

        var retryCounter = 0;
        Func<Task<RestResponse>> action = () =>
        {
            retryCounter++;
            return Task.FromResult(successResponse);
        };

        // Act
        var result = await retryPolicy.ExecuteAsync(action);

        // Assert
        retryCounter.ShouldBe(1); 
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}