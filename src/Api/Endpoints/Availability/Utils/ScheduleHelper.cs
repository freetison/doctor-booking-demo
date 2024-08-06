using Api.Endpoints.Availability.Extensions;
using Api.Endpoints.Availability.Models.Domain;

namespace Api.Endpoints.Availability.Utils;

public partial class ScheduleHelper
{
    private DateTime WorkStart { get; }
    private DateTime WorkEnd { get; }
    private DateTime LunchStart { get; }
    private DateTime LunchEnd { get; }
    private int SlotDurationMinutes { get; }

    public ScheduleHelper(WorkPeriod? workPeriod, string inputDate, int slotDurationMinutes)
    {
        if (workPeriod != null)
        {
            WorkStart = GetDateTimeFor(inputDate, workPeriod.StartHour);
            WorkEnd = GetDateTimeFor(inputDate, workPeriod.EndHour);
            LunchStart = GetDateTimeFor(inputDate, workPeriod.LunchStartHour);
            LunchEnd = GetDateTimeFor(inputDate, workPeriod.LunchEndHour);
        }

        SlotDurationMinutes = slotDurationMinutes;
    }


    private static DateTime GetDateTimeFor(string inputDate, int value) => DateTime.ParseExact($"{inputDate}T{value:D2}:00:00", "yyyyMMddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

    public List<FreeSlot> GetFreeSlots(List<BusySlot>? busySlots)
    {
        var freeSlots = new List<FreeSlot>();
        if (busySlots == null)
        {
            return freeSlots;
        }
        
        // Generate all available time slots for the day, excluding lunch hours.
        AddSlots(WorkStart, LunchStart);
        AddSlots(LunchEnd, WorkEnd);

        // Subtract the busy intervals from the free intervals using the extension
        return freeSlots.SubtractBusySlots(busySlots);

        void AddSlots(DateTime periodStart, DateTime periodEnd)
        {
            var slotStart = periodStart;

            while (slotStart.AddMinutes(SlotDurationMinutes) <= periodEnd)
            {
                freeSlots.Add(new FreeSlot { Start = slotStart, End = slotStart.AddMinutes(SlotDurationMinutes) });
                slotStart = slotStart.AddMinutes(SlotDurationMinutes);
            }
        }
    }


}
