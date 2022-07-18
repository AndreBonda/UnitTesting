using LogAn.Interfaces;

namespace LogAn.UnitTests.Fakes;

public class FakeStubWebService : IWebService
{
    public Exception ThrowException { get; set; }
    
    public void LogError(string message)
    {
        if (ThrowException != null) throw ThrowException;
    }
}