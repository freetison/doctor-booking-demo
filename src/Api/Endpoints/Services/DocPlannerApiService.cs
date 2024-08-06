using Api.Endpoints.Appointments.Domain;
using Api.Endpoints.Availability.Extensions;

using RestSharp;

using System.Net;

using Tx.Core.Extensions.String;

namespace Api.Endpoints.Services;

public class DocPlannerApiService(IRestClient restClient, ILogger<DocPlannerApiService> logger) : IDocPlannerApiService
{
    private const string WeeklyAvailabilityEndpoint = "/api/availability/GetWeeklyAvailability";

    public async Task<string?> GetWeeklyAvailability(string date, CancellationToken ct)
    {
        var request = new RestRequest($"{WeeklyAvailabilityEndpoint}/{date}");
        request.AddHeader("Accept", "*/*");
        request.AddHeader("Content-Type", "application/json");

        var result = await PollyExtensions.HttpPolicy()
            .ExecuteAsync(async () =>
            {
                var response = await restClient.ExecuteAsync(request, ct);
                return response;

            });

        if (result.StatusCode == HttpStatusCode.OK) return result.Content;
        var msg = $"Request failed with status code {result.StatusCode}";
        logger.LogError(msg);
        throw new Exception(msg);

    }

    public async Task TakeSlot(Appointment appointment, CancellationToken ct)
    {
        var request = new RestRequest("/api/availability/TakeSlot", Method.Post);
        request.AddHeader("Accept", "*/*");
        request.AddHeader("Content-Type", "application/json");
        request.AddStringBody(appointment.ToJson(), DataFormat.Json);

        var result = await PollyExtensions.HttpPolicy()
            .ExecuteAsync(async () =>
            {
                var response = await restClient.ExecuteAsync(request, ct);
                return response;

            });

        if (result.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception($"Request failed with status code {result.StatusCode}");
        }

    }
}