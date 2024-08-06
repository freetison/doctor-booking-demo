using System.Diagnostics;
using Api.Common.Exceptions;
using Api.Endpoints.Availability.Events;
using Api.Endpoints.Availability.Extensions;
using Api.Endpoints.Availability.Models;
using Api.Endpoints.Availability.Models.Domain;
using Api.Endpoints.Availability.PreProcessors;
using Api.Endpoints.Availability.Utils;
using Api.Endpoints.Availability.Validators;
using Api.Endpoints.Services;

using FastEndpoints;
using Tx.Core.Extensions.String;

namespace Api.Endpoints.Availability;

public class GetWeeklyAvailabilityEndpoint : Endpoint<WeeklyAvailabilityRequest, FreeWeekSchedule>
{
    private readonly IDocPlannerApiService _docPlannerApiService;
    private readonly IFreeSlotProcessor _freeSlotProcessor;

    public GetWeeklyAvailabilityEndpoint(IDocPlannerApiService docPlannerApiService, IFreeSlotProcessor freeSlotProcessor)
    {
        _docPlannerApiService = docPlannerApiService;
        _freeSlotProcessor = freeSlotProcessor;
    }

    public override void Configure()
    {
        Get("api/weekly-availability/{date}");
        AllowAnonymous();
        Validator<DateRequestValidator>();
        PreProcessor<AvailabilityPreProcessor>();
    }

    public override async Task<FreeWeekSchedule> ExecuteAsync(WeeklyAvailabilityRequest req, CancellationToken ct)
    {
        var result = await _docPlannerApiService.GetWeeklyAvailability(req.Date, ct);
        if (result == null) { throw new NotFoundException(); }

        WeekDaySchedule? weekDaySchedule = _freeSlotProcessor.ToWeekDaySchedule(result);
        
        if(weekDaySchedule == null || weekDaySchedule.Days.Count == 0) { throw new NotDataToProceedException(); }
        
        var freeSlots = _freeSlotProcessor.FindFreeSlots(weekDaySchedule, req.Date);
        Debug.WriteLine(freeSlots.ToJson());
        var freeWeekSchedule  = freeSlots.ToFreeWeekSchedule(weekDaySchedule.Facility);

        await PublishAsync(new AvailabilityProcessedEvent(freeWeekSchedule), cancellation: ct);

        return await Task.FromResult(freeWeekSchedule);
    }


}