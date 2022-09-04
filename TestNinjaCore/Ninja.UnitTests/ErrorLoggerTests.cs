using NUnit.Framework;
using TestNinja.Fundamentals;

namespace Ninja.UnitTests;

[TestFixture]
public class ErrorLoggerTests
{
    private ErrorLogger _logger;
    
    [SetUp]
    public void SetUp()
    {
        _logger = new ErrorLogger();
    }

    [Test]
    public void ErrorLogger_ValidLogMessage_SetLastErrorProperty()
    {
        string logMessage = "msg";
        
        _logger.Log(logMessage);

        Assert.That(_logger.LastError, Is.EqualTo(logMessage));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void ErrorLogger_MessageIsNullOrWhiteSpace_ThrowArgumentNullException(string msg)
    {
        Assert.That(() => _logger.Log(msg), Throws.Exception.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void ErrorLogger_ValidLogMessage_RaiseLoggedEvent()
    {
        var id = Guid.Empty;
        // registra un subscriber all'event handler
        _logger.ErrorLogged += (sender, args) =>
        {
            id = args;
        };
        _logger.Log("msg");
        
        // se id non è più empty, significa che l'evento è stato inviato
        Assert.That(id, Is.Not.EqualTo(Guid.Empty));
    }
    
    
}