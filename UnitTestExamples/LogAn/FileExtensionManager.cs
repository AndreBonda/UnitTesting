using LogAn.Interfaces;

namespace LogAn;

public class FileExtensionManager : IFileExtensionManager
{
    public bool IsValid(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("filename has to be provided");
        
        string content =
            File.ReadAllText("/home/bonda/Repo/UnitTesting/UnitTestExamples/LogAn/availableFileExtensions.txt");
        //bool result = fileName.EndsWith(".slf", StringComparison.CurrentCultureIgnoreCase);
        string fileExtension = fileName.Split('.')[1];
        return content.Split(',').Contains(fileExtension.ToLower());
    }
}