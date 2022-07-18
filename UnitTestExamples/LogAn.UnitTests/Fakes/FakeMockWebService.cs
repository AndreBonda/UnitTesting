using LogAn.Interfaces;

namespace LogAn.UnitTests.Fakes;

/// <summary>
/// Questo è un mock object. Viene utilizzato per testare l'interazione tra
/// LogAnalyzer e il web-service.
/// </summary>
public class FakeMockWebService : IWebService
{
    public string LastErrorMsg { get; set; }
    
    public void LogError(string message)
    {
        LastErrorMsg = message;
    }
}