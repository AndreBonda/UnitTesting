using LogAn.Interfaces;

namespace LogAn;

public class LogAnalyzer
{
    public bool WasLastExtenxionValid { get; set; }

    private readonly IFileExtensionManager _fsManager;

    public LogAnalyzer(IFileExtensionManager fsManager)
    {
        _fsManager = fsManager;
    }
    
    public bool IsValidLogFileName(string fileName)
    {
        bool result = _fsManager.IsValid(fileName);
        WasLastExtenxionValid = result;
        return result;
    }
}