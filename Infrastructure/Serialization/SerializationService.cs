using Application.Interfaces;
using SerializationPlugin;

namespace Infrastructure.Serialization;

public class SerializationService
{
    public string Serialize<T>(T objectToSerialize, string format)
    {
        return SerializerFactory.GetSerializer(format).Serialize(objectToSerialize);
    }

    public T? Deserialize<T>(string data, string format)
    {
        return SerializerFactory.GetSerializer(format).Deserialize<T>(data);
    }

    // ASYNC serialization (file)

    public async Task SerializeFileAsync<T>(T objectToSerialize, string filePath, string format)
    {
        await FileSerializer.SaveToFileAsync(objectToSerialize, filePath, format);
    }

    public async Task<T?> DeserializeFileAsync<T>(string filePath, string format) where T : new()
    {
        return await FileSerializer.LoadFromFileAsync<T?>(filePath, format);
    }
}
