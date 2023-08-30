using System.Collections;
using System.Text;
using System.Text.Json;

namespace BackwardsCompatibility;

public class JavaScriptSerializer
{
    public JavaScriptSerializer() { }

    public string Serialize(object obj)
    {
        using var stream = new MemoryStream();
        JsonSerializer.Serialize(stream, obj);
        return Encoding.UTF8.GetString(stream.ToArray());
    }

    private static object? ConvertJsonElementToObject(JsonElement jsonElement)
    {
        return jsonElement.ValueKind switch
        {
            JsonValueKind.Undefined => jsonElement.GetString(),
            JsonValueKind.Object => jsonElement.EnumerateObject()
                .Select(prop => (Key: prop.Name, Value: ConvertJsonElementToObject(prop.Value)))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
            JsonValueKind.Array => new ArrayList(jsonElement.EnumerateArray().Select(ConvertJsonElementToObject).ToArray()),
            JsonValueKind.String => jsonElement.GetString(),
            JsonValueKind.Number => jsonElement.GetDouble(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null => null,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public T? Deserialize<T>(string str)
    {
        var result = JsonSerializer.Deserialize<T>(str);
        if (result is Dictionary<string, object?> dictionary)
        {
            foreach (var kvp in dictionary.ToArray())
            {
                if (dictionary[kvp.Key] is not JsonElement jsonElement) { continue; }

                dictionary[kvp.Key] = ConvertJsonElementToObject(jsonElement);
            }
        }
        return result;
    }
}