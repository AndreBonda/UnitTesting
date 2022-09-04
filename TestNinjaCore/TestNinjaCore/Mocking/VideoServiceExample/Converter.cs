using Newtonsoft.Json;

namespace TestNinjaCore.Mocking.VideoServiceExample;

public class Converter<T> : IConverter<T>
{
    public T Deserialize(string source)
    {
        return JsonConvert.DeserializeObject<T>(source);
    }
}