using LogAn.Interfaces;
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

    [Test]
    public void IsValidLogFileName_SupportedExtension_ReturnTrue()
    {
        FakeExtensionManager fakeExtensionManager = new FakeExtensionManager();
        fakeExtensionManager.ValidExtension = true;
        LogAnalyzer analyzer = new LogAnalyzer(fakeExtensionManager);
        bool result = analyzer.IsValidLogFileName("file.valid-extension");
        
        Assert.True(result);
    }

    [Test]
    public void IsValidLogFileName_UnsupportedExtension_ReturnFalse()
    {
        FakeExtensionManager fakeExtensionManager = new FakeExtensionManager();
        fakeExtensionManager.ValidExtension = false;
        LogAnalyzer analyzer = new LogAnalyzer(fakeExtensionManager);
        bool result = analyzer.IsValidLogFileName("file.not-valid-extension");
        
        Assert.False(result);
    }
    
    [Test]
    public void IsValidLogFileName_AfterInvoke_ChangeWasLastFileNameValid()
    {
        FakeExtensionManager fakeExtensionManager = new FakeExtensionManager();
        fakeExtensionManager.ValidExtension = true;
        LogAnalyzer analyzer = new LogAnalyzer(fakeExtensionManager);
        bool result = analyzer.IsValidLogFileName("file.valid-extension");
        
        Assert.True(analyzer.WasLastExtenxionValid);
    }
    
    [Test]
    public void IsValidLogFileName_AfterInvoke_ChangeWasLastFileNameNotValid()
    {
        FakeExtensionManager fakeExtensionManager = new FakeExtensionManager();
        fakeExtensionManager.ValidExtension = false;
        LogAnalyzer analyzer = new LogAnalyzer(fakeExtensionManager);
        bool result = analyzer.IsValidLogFileName("file.valid-extension");
        
        Assert.False(analyzer.WasLastExtenxionValid);
    }
}

// Stub class. Ci permette di bypassare la dipendenza del file-system che abbiamo nella reale implementazione FileExtensionManager.
internal class FakeExtensionManager : IFileExtensionManager
{
    /// <summary>
    /// Valorizzato dai test-cases per simulare il risultato della reale implementazione FileExtensionManager
    /// </summary>
    public bool ValidExtension { get; set; }
    
    public bool IsValid(string fileName)
    {
        return ValidExtension;
    }
}