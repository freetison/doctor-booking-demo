using Api.Endpoints.Availability.Models.Domain;

namespace Api.Endpoints.Availability.Utils;

public interface IFreeSlotProcessor
{
    List<(string Day, List<FreeSlot>)> FindFreeSlots(WeekDaySchedule weeklySchedule, string date);
    WeekDaySchedule? ToWeekDaySchedule(string json);
}