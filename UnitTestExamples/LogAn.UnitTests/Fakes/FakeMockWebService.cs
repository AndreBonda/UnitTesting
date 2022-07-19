using LogAn.Interfaces;

namespace LogAn.UnitTests.Fakes;

/// <summary>
/// Questo è un mock object. Viene utilizzato per testare l'interazione tra
/// LogAnalyzer e il web-service.
///
/// Negli esempi successivi questo mock "scritto manualmente" (hand-written mock) è stato rimpiazzato
/// da un mock scritto dinamicamente con NSubstitute (Isolation framework)
/// </summary>
public class FakeMockWebService : IWebService
{
    public string LastErrorMsg { get; set; }
    
    public void LogError(string message)
    {
        LastErrorMsg = message;
    }
}