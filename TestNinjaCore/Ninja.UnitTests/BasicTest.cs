using NUnit.Framework;

namespace Ninja.UnitTests;

/// <summary>
/// Testing di alcuni tipi di dato. Non legati alle classi nel progetto Ninja
/// </summary>
[TestFixture]
public class BasicTest
{
    [Test]
    public void CheckSimpleString()
    {
        var result = "Andrea";
        
        Assert.That(result, Is.EqualTo("Andrea"));
        Assert.That(result, Is.Not.EqualTo("andrea"));
        Assert.That(result, Is.EqualTo("andrea").IgnoreCase);
    }
}