using LogAn.Interfaces;

namespace LogAn;

public class LogAnalyzer
{
    /// <summary>
    /// Caching last processing
    /// </summary>
    public bool WasLastExtenxionValid { get; set; }

    private readonly IFileExtensionManager _fsManager;
    private readonly IWebService _webService;
    private readonly IEmailService _emailService;
    public LogAnalyzer(IFileExtensionManager fsManager, IWebService webService, IEmailService emailService)
    {
        _fsManager = fsManager;
        _webService = webService;
        _emailService = emailService;
    }
    
    public bool IsValidLogFileName(string fileName)
    {
        if (IsFilaNameTooShort(fileName)) return false;
        
        bool result = _fsManager.IsValid(fileName);
        WasLastExtenxionValid = result;
        return result;
    }

    private bool IsFilaNameTooShort(string fileName)
    {
        if (fileName.Length <= 2)
        {
            try
            {
                _webService.LogError("Too short");
            }
            catch (Exception e)
            {
                _emailService.SendEmail("someone@somewhere.com", "can't log", e.Message);
            }
            return true;
        }
        return false;
    }
}