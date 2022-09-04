using NUnit.Framework;
using TestNinja.Fundamentals;

namespace Ninja.UnitTests;

[TestFixture]
public class HtmlFormatterTests
{
    [TestCase("input")]
    public void FormatAsBold_WhenCalled_ReturnCorrectFormat(string content)
    {
        var result = new HtmlFormatter().FormatAsBold(content);
        
        Assert.That(result, Does.Match(@"<strong>\w+<\/strong>"));
    }
}