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

    public T? Deserialize<T>(string str)
    {
        return JsonSerializer.Deserialize<T>(str);
    }
}