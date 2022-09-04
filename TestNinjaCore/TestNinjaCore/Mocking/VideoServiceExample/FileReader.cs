namespace TestNinjaCore.Mocking.VideoServiceExample;

public class FileReader : IFileReader
{
    public string ReadFileContent(string path)
    {
        return File.ReadAllText(path);
    }
}