using System.Text.Json;
using System.Text.Json.Serialization;

namespace Group.Common.Converter;

public class TupleConverter: JsonConverter<List<(Guid, Guid)>>
{
    public override List<(Guid, Guid)> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            var list = new List<(Guid, Guid)>();

            foreach (var item in root.EnumerateArray())
            {
                Guid guid1 = Guid.Parse(item[0].GetString());
                Guid guid2 = Guid.Parse(item[1].GetString());
                list.Add((guid1, guid2));
            }

            return list;
        }
    }

    public override void Write(Utf8JsonWriter writer, List<(Guid, Guid)> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var item in value)
        {
            writer.WriteStartArray();
            writer.WriteStringValue(item.Item1.ToString()); // Преобразование Guid в строку
            writer.WriteStringValue(item.Item2.ToString());
            writer.WriteEndArray();
        }

        writer.WriteEndArray();
    }
}