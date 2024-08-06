using System.Text.Json;

namespace Api.Endpoints.Availability.Utils;

public class JsonDeserializer : IJsonDeserializer
{
    public T? Deserialize<T>(string json, JsonSerializerOptions? options)
    {
        return JsonSerializer.Deserialize<T>(json, options);
    }
}