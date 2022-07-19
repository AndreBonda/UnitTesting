using LogAn.Interfaces;
using LogAn.UnitTests.Fakes;
using NSubstitute;
using NUnit.Framework;

namespace LogAn.UnitTests;

/**
 * Per questo test è stato utilizzato la tecnica che inietta uno stub tramite costruttore.
 * Configurando lo stub è possibile emulare i vari comportamenti della reale implementazione.
 * Libro 'The art of unit testing' --> capitolo 3.4.3
 *
 * In un primo esempio gli stubs e i sono stati implementati manualmente, successivamente rimpiazzati
 * dall'utilizzo di un isolation framework che genera i fake dinamicamente.
 *
 * Il codice dei fake con NSubstitute viene generato a Runtime.
 */

[TestFixture]
public class LogAnalyzerTests
{
    private IFileExtensionManager _fakeFileExtensionManager;
    private IWebService _fakeWebService;
    private IEmailService _fakeEmailService;

    [SetUp]
    public void InitTest()
    {
        _fakeFileExtensionManager = Substitute.For<IFileExtensionManager>();
        _fakeWebService = Substitute.For<IWebService>();
        _fakeEmailService = Substitute.For<IEmailService>();
    }

    [Test]
    public void IsValidLogFileName_SupportedExtension_ReturnTrue()
    {
        _fakeFileExtensionManager
            .IsValid(Arg.Any<string>())
            .Returns(true);
        LogAnalyzer analyzer = new LogAnalyzer(_fakeFileExtensionManager, _fakeWebService, null);
        bool result = analyzer.IsValidLogFileName("file.valid-extension");
        
        Assert.True(result);
    }

    [Test]
    public void IsValidLogFileName_UnsupportedExtension_ReturnFalse()
    {
        _fakeFileExtensionManager
            .IsValid(Arg.Any<string>())
            .Returns(false);
        LogAnalyzer analyzer = new LogAnalyzer(_fakeFileExtensionManager, _fakeWebService, null);
        bool result = analyzer.IsValidLogFileName("file.not-valid-extension");
        
        Assert.False(result);
    }
    
    [Test]
    public void IsValidLogFileName_SupportedExtension_ChangeWasLastFileNameValid()
    {
        _fakeFileExtensionManager
            .IsValid(Arg.Any<string>())
            .Returns(true);
        LogAnalyzer analyzer = new LogAnalyzer(_fakeFileExtensionManager, _fakeWebService, null);
        analyzer.IsValidLogFileName("file.valid-extension");
        
        Assert.True(analyzer.WasLastExtenxionValid);
    }
    
    [Test]
    public void IsValidLogFileName_AfterInvoke_ChangeWasLastFileNameNotValid()
    {
        _fakeFileExtensionManager
            .IsValid(Arg.Any<string>())
            .Returns(false);
        LogAnalyzer analyzer = new LogAnalyzer(_fakeFileExtensionManager, _fakeWebService, null);
        analyzer.IsValidLogFileName("file.valid-extension");
        
        Assert.False(analyzer.WasLastExtenxionValid);
    }

    [TestCase("a")]
    [TestCase("bb")]
    public void IsValidLogFileName_TooShortFileName_CallWebService(string fileName)
    {
        LogAnalyzer analyzer = new LogAnalyzer(_fakeFileExtensionManager, _fakeWebService, null);
        analyzer.IsValidLogFileName(fileName);
        
        _fakeWebService.Received().LogError("Too short");
    }

    [TestCase("longEnough")]
    public void IsValidLogFileName_ExpectedLengthFileName_NoCallWebService(string fileName)
    {
        LogAnalyzer analyzer = new LogAnalyzer(_fakeFileExtensionManager, _fakeWebService, null);
        analyzer.IsValidLogFileName(fileName);

        _fakeWebService.DidNotReceive().LogError(null);
    }

    [TestCase("a")]
    public void IsValidLogFileName_WebServiceThrow_SendEmail(string fileName)
    {
        //_fakeStubWebServiceOld.ThrowException = new Exception("fake exception");
        _fakeWebService
            .When(x => x.LogError(Arg.Any<string>()))
            .Do(context => throw new Exception("Fake exception"));
        
        LogAnalyzer analyzer = new LogAnalyzer(_fakeFileExtensionManager, _fakeWebService, _fakeEmailService);
        analyzer.IsValidLogFileName(fileName);
        
        // qui le stringhe devono essere uguali
        _fakeEmailService.Received().SendEmail("someone@somewhere.com", "can't log", "Fake exception");
        
        // se non voglio confrontare tutta la stringa ma verificare che contengano delle parole chiave all'interno.
        _fakeEmailService.Received().SendEmail(Arg.Is<string>(s => s.Contains("someone")), 
            Arg.Is<string>(s => s.ToLower().Contains("can't log")), 
            Arg.Is<string>(s => s.ToLower().Contains("fake")));
    }
}