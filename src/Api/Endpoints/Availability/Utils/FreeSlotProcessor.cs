using Api.Endpoints.Availability.Extensions;
using Api.Endpoints.Availability.Models.Domain;
using Tx.Core.Extensions.String;

namespace Api.Endpoints.Availability.Utils;

public class FreeSlotProcessor: IFreeSlotProcessor
{
    public List<(string Day, List<FreeSlot>)> FindFreeSlots(WeekDaySchedule? weekDaySchedule, string inputDate)
    {
        var freeSlots = new List<(string Day, List<FreeSlot>)>();
        if (weekDaySchedule?.Days == null) return freeSlots;
        
        foreach (var (dayOfWeek, value) in weekDaySchedule.Days)
        {
            var workPeriod = value?.WorkPeriod;
            var busySlots = value?.BusySlots;

            var scheduleHelper = new ScheduleHelper(workPeriod, inputDate, weekDaySchedule.SlotDurationMinutes);
            var freeSlotsNoBusy = scheduleHelper.GetFreeSlots(busySlots);

            freeSlots.Add((dayOfWeek, freeSlotsNoBusy));

            foreach (var slot in freeSlotsNoBusy)
            {
                Console.WriteLine($"Free slot from {slot.Start} to {slot.End}");
            }
        }

        return freeSlots;
    }

    public WeekDaySchedule? ToWeekDaySchedule(string json)
    {
        return  json
            .ParseTo<WeeklySchedule>()
            .ToWeekDaySchedule()
            .FilterDays();
    }

    private static DateTime GetDateTimeFor(string inputDate, int value) => DateTime.ParseExact($"{inputDate}T{value:D2}:00:00", "yyyyMMddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
}