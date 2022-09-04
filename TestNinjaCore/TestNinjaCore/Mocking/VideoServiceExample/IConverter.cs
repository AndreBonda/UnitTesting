namespace TestNinjaCore.Mocking.VideoServiceExample;

public interface IConverter<T>
{
    T Deserialize(string source);
}