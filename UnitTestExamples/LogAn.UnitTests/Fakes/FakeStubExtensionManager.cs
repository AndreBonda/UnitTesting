using LogAn.Interfaces;

namespace LogAn.UnitTests.Fakes;

// Stub class. Ci permette di bypassare la dipendenza del file-system che abbiamo nella reale implementazione FileExtensionManager.
public class FakeStubExtensionManager : IFileExtensionManager
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