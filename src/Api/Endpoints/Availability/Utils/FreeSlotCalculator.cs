using Api.Endpoints.Availability.Models.Domain;

namespace Api.Endpoints.Availability.Utils;
public class FreeSlotCalculator
{
    public static Dictionary<string, List<Slot>> CalculateAvailableSlots(DateTime baseDate, FacilitySchedule schedule)
    {
        var availableSlots = new Dictionary<string, List<Slot>>();

        // Calcular el inicio de la semana (lunes)
        int daysOffset = ((int)DayOfWeek.Monday - (int)baseDate.DayOfWeek + 7) % 7;
        DateTime weekStart = baseDate.AddDays(daysOffset);

        // Iterar solo de lunes a viernes
        for (int i = 0; i < 5; i++)
        {
            DateTime currentDay = weekStart.AddDays(i);
            string dayName = currentDay.DayOfWeek.ToString();

            if (!schedule.Days.TryGetValue(dayName, out DaySchedule? daySchedule))
            {
                continue;
            }

            List<BusySlot>? busySlots = daySchedule?.BusySlots.OrderBy(s => s.Start).ToList();
            WorkPeriod? workPeriod = daySchedule?.WorkPeriod;

            List<Slot> dayAvailableSlots = [];

            DateTime workStart = new DateTime(currentDay.Year, currentDay.Month, currentDay.Day, workPeriod.StartHour, 0, 0);
            DateTime workEnd = new DateTime(currentDay.Year, currentDay.Month, currentDay.Day, workPeriod.EndHour, 0, 0);
            DateTime lunchStart = new DateTime(currentDay.Year, currentDay.Month, currentDay.Day, workPeriod.LunchStartHour, 0, 0);
            DateTime lunchEnd = new DateTime(currentDay.Year, currentDay.Month, currentDay.Day, workPeriod.LunchEndHour, 0, 0);

            DateTime currentStart = workStart;

            while (currentStart < workEnd)
            {
                DateTime currentEnd = currentStart.AddMinutes(schedule.SlotDurationMinutes);

                if (currentEnd > workEnd)
                {
                    break;
                }

                // Check for lunch break overlap
                if ((currentStart < lunchStart && currentEnd > lunchStart) || (currentStart >= lunchStart && currentStart < lunchEnd))
                {
                    currentStart = lunchEnd;
                    continue;
                }

                // Check if current slot overlaps with any busy slot
                bool isBusy = busySlots.Any(slot => slot.Start < currentEnd && slot.End > currentStart);

                if (!isBusy)
                {
                    dayAvailableSlots.Add(new Slot { Start = currentStart, End = currentEnd });
                }

                currentStart = currentStart.AddMinutes(schedule.SlotDurationMinutes);
            }

            availableSlots[dayName] = dayAvailableSlots;
        }

        return availableSlots;
    }
}