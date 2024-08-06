using Api.Endpoints.Availability.Models.Domain;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api.Endpoints.Availability.Utils;

/// <summary>
/// 
/// </summary>
public class FacilityScheduleJsonConverter : JsonConverter<FacilitySchedule>
{
    public override FacilitySchedule Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
    {
        var facilitySchedule = new FacilitySchedule();
        var weeklySchedule = new Dictionary<string, DaySchedule?>();

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string? propertyName = reader.GetString();
            reader.Read();

            switch (propertyName?.ToLower())
            {
                case "facility":
                    facilitySchedule.Facility = JsonSerializer.Deserialize<Facility>(ref reader, options);
                    break;
                case "slotdurationminutes":
                    facilitySchedule.SlotDurationMinutes = reader.GetInt32();
                    break;
                default:
                    if (propertyName != null)
                    {
                        var dayNames = Enum.GetNames(typeof(DayOfWeek));
                        bool exists = Array.Exists(dayNames, day => day.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
                        if (exists)
                        {
                            weeklySchedule[propertyName] = JsonSerializer.Deserialize<DaySchedule>(ref reader, options);

                        }
                    }
                    break;
            }
        }

        foreach (var day in Enum.GetNames(typeof(DayOfWeek)))
        {
            if (!weeklySchedule.ContainsKey(day))
            {
                weeklySchedule[day] = new DaySchedule
                {
                    WorkPeriod = new WorkPeriod
                    {
                        StartHour = 9,
                        EndHour = 17
                    },
                    BusySlots = new List<BusySlot>(),
                };
            }
        }

        facilitySchedule.Days = weeklySchedule;
        return facilitySchedule;
    }

    public override void Write(Utf8JsonWriter writer, FacilitySchedule value, JsonSerializerOptions? options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("facility");
        JsonSerializer.Serialize(writer, value.Facility, options);

        writer.WritePropertyName("slotDurationMinutes");
        writer.WriteNumberValue(value.SlotDurationMinutes);

        writer.WritePropertyName("weeklySchedule");
        writer.WriteStartObject();
        foreach (var kvp in value.Days)
        {
            writer.WritePropertyName(kvp.Key);
            JsonSerializer.Serialize(writer, kvp.Value, options);
        }
        writer.WriteEndObject();

        writer.WriteEndObject();
    }


}