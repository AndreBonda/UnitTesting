using LogAn.Interfaces;
using LogAn.UnitTests.Fakes;
using NUnit.Framework;

namespace LogAn.UnitTests;

/**
 * Per questo test è stato utilizzato la tecnica che inietta uno stub tramite costruttore.
 * Configurando lo stub è possibile emulare i vari comportamenti della reale implementazione.
 * Libro 'The art of unit testing' --> capitolo 3.4.3
 */

[TestFixture]
public class LogAnalyzerTests
{
    //Stubs
    private FakeStubExtensionManager _fakeStubExtensionManager;
    private FakeStubWebService _fakeStubWebService;
    
    //Mocks
    private FakeMockWebService _fakeMockWebSvc;
    private FakeMockEmailService _fakeMockEmailService;

    [SetUp]
    public void InitTest()
    {
        _fakeStubExtensionManager = new FakeStubExtensionManager();
        _fakeStubWebService = new FakeStubWebService();
        _fakeMockWebSvc = new FakeMockWebService();
        _fakeMockEmailService = new FakeMockEmailService();
    }

    [Test]
    public void IsValidLogFileName_SupportedExtension_ReturnTrue()
    {
        _fakeStubExtensionManager.ValidExtension = true;
        LogAnalyzer analyzer = new LogAnalyzer(_fakeStubExtensionManager, null, null);
        bool result = analyzer.IsValidLogFileName("file.valid-extension");
        
        Assert.True(result);
    }

    [Test]
    public void IsValidLogFileName_UnsupportedExtension_ReturnFalse()
    {
        _fakeStubExtensionManager.ValidExtension = false;
        LogAnalyzer analyzer = new LogAnalyzer(_fakeStubExtensionManager, null, null);
        bool result = analyzer.IsValidLogFileName("file.not-valid-extension");
        
        Assert.False(result);
    }
    
    [Test]
    public void IsValidLogFileName_AfterInvoke_ChangeWasLastFileNameValid()
    {
        _fakeStubExtensionManager.ValidExtension = true;
        LogAnalyzer analyzer = new LogAnalyzer(_fakeStubExtensionManager, null, null);
        bool result = analyzer.IsValidLogFileName("file.valid-extension");
        
        Assert.True(analyzer.WasLastExtenxionValid);
    }
    
    [Test]
    public void IsValidLogFileName_AfterInvoke_ChangeWasLastFileNameNotValid()
    {
        _fakeStubExtensionManager.ValidExtension = false;
        LogAnalyzer analyzer = new LogAnalyzer(_fakeStubExtensionManager, null, null);
        bool result = analyzer.IsValidLogFileName("file.valid-extension");
        
        Assert.False(analyzer.WasLastExtenxionValid);
    }

    [TestCase("a")]
    [TestCase("bb")]
    public void IsValidLogFileName_TooShortFileName_CallWebService(string fileName)
    {
        LogAnalyzer analyzer = new LogAnalyzer(_fakeStubExtensionManager, _fakeMockWebSvc, null);
        analyzer.IsValidLogFileName(fileName);
        
        StringAssert.Contains("short", _fakeMockWebSvc.LastErrorMsg);
    }

    [TestCase("longEnogh")]
    public void IsValidLogFileName_ExpectedLengthFileName_NoCallWebService(string fileName)
    {
        LogAnalyzer analyzer = new LogAnalyzer(_fakeStubExtensionManager, _fakeMockWebSvc, null);
        analyzer.IsValidLogFileName(fileName);

        Assert.Null(_fakeMockWebSvc.LastErrorMsg);
    }

    [TestCase("a")]
    public void IsValidLogFileName_WebServiceFail_SendEmail(string fileName)
    {
        _fakeStubWebService.ThrowException = new Exception("fake exception");
        LogAnalyzer analyzer = new LogAnalyzer(null, _fakeStubWebService, _fakeMockEmailService);
        analyzer.IsValidLogFileName(fileName);
        
        StringAssert.Contains("someone@somewhere.com", _fakeMockEmailService.To);
        StringAssert.Contains("can't log", _fakeMockEmailService.Subject);
        StringAssert.AreEqualIgnoringCase("fake exception", _fakeMockEmailService.Body);
    }


}