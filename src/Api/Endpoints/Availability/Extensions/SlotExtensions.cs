using Api.Endpoints.Availability.Models.Domain;

using static Api.Endpoints.Availability.Utils.ScheduleHelper;

namespace Api.Endpoints.Availability.Extensions;
public static class SlotExtensions
{
    public static List<FreeSlot> SubtractBusySlots(this IEnumerable<FreeSlot> freeSlots, List<BusySlot>? busySlots)
    {
        var busySlotsAsFreeSlots = busySlots.Select(b => new FreeSlot { Start = b.Start, End = b.End }).ToList();
        return freeSlots.Except(busySlotsAsFreeSlots, new SlotComparer()).ToList();
    }

    public static FreeWeekSchedule ToFreeWeekSchedule(this List<(string Day, List<FreeSlot>)> freeSlots, Facility facility)
    {
        var dayFreeSlots = freeSlots.Select(fs => new DayFreeSlots { Day = fs.Item1, FreeSlots = fs.Item2 }).ToList();
        return new FreeWeekSchedule
        {
            Facility = facility,
            FreeSlots = dayFreeSlots
        };
    }
}