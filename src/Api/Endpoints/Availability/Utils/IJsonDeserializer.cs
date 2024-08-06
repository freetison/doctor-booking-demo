using System.Text.Json;

namespace Api.Endpoints.Availability.Utils;

public interface IJsonDeserializer
{
    T? Deserialize<T>(string json, JsonSerializerOptions? options);
}