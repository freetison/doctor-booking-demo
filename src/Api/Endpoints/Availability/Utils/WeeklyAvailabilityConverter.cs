using Api.Endpoints.Availability.Models.Domain;

using Newtonsoft.Json.Linq;

namespace Api.Endpoints.Availability.Utils;

public class WeeklyAvailabilityConverter
{
    public static WeeklyAvailability ConvertFromJson(string json)
    {
        var weeklyAvailability = new WeeklyAvailability();
        var jsonObject = JObject.Parse(json);

        var facilityToken = GetProperty<Facility>(jsonObject, "Facility");
        if (facilityToken != null)
        {
            weeklyAvailability.Facility = facilityToken;
        }

        foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
        {
            var dayKey = dayOfWeek.ToString();

            if (!jsonObject.TryGetValue(dayKey, out JToken? dayToken) || dayToken.Type == JTokenType.Null) continue;
            var dayAvailability = new DayAvailability
            {
                DayOfWeek = dayOfWeek.ToString(),
                WorkPeriod = GetProperty<WorkPeriod>(dayToken, "WorkPeriod"),
                BusySlots = GetProperty<List<Slot>>(dayToken, "BusySlots")
            };

            weeklyAvailability.Days.Add(dayAvailability);
        }

        return weeklyAvailability;
    }

    private static T? GetProperty<T>(JObject jsonObject, string key) where T : class
    {
        try
        {
            var token = jsonObject[key];
            return token?.ToObject<T>();

        }
        catch (Exception)
        {
            return null;
        }
    }

    private static T? GetProperty<T>(JToken? jsonToken, string key) where T : class
    {
        try
        {
            var token = jsonToken[key];
            return token?.ToObject<T>();
        }
        catch (Exception)
        {
            return null;
        }
    }
}