using Api.Endpoints.Availability.Models.Domain;

namespace Api.Endpoints.Availability.Extensions;

public static class ConverterExtensions
{
    public static WeekDaySchedule ToWeekDaySchedule(this WeeklySchedule weeklySchedule)
    {
        var weekDaySchedule = new WeekDaySchedule
        {
            Facility = weeklySchedule.Facility,
            SlotDurationMinutes = weeklySchedule.SlotDurationMinutes
        };

        foreach (var day in weeklySchedule.Days)
        {
            if (day.Value is not Newtonsoft.Json.Linq.JObject dayObject) continue;
            var daySchedule = dayObject.ToObject<DaySchedule>();
            weekDaySchedule.Days.Add(day.Key, daySchedule);
        }

        return weekDaySchedule;
    }

    public static WeekDaySchedule? FilterDays(this WeekDaySchedule schedule)
    {
        var filteredDays = schedule.Days
            .Where(day => day.Value is { WorkPeriod: not null })
            .ToDictionary(day => day.Key, day => day.Value);

        return new WeekDaySchedule
        {
            Facility = schedule.Facility,
            SlotDurationMinutes = schedule.SlotDurationMinutes,
            Days = filteredDays
        };
    }

}