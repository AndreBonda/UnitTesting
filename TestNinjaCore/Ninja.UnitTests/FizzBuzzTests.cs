using NUnit.Framework;
using TestNinja.Fundamentals;

namespace Ninja.UnitTests;

[TestFixture]
public class FizzBuzzTests
{

    [Test]
    public void GetOutput_NumberIsMultipleOf3And5_ReturnsFizzBuzz()
    {
        int number = 15;

        string result = FizzBuzz.GetOutput(number);
        
        Assert.That(result, Is.EqualTo("FizzBuzz"));
    }
    
    [Test]
    public void GetOutput_NumberIsOnlyMultipleOf3_ReturnsFizz()
    {
        int number = 3;

        string result = FizzBuzz.GetOutput(number);
        
        Assert.That(result, Is.EqualTo("Fizz"));
    }
    
    [Test]
    public void GetOutput_NumberIsOnlyMultipleOf5_ReturnsBuzz()
    {
        int number = 5;

        string result = FizzBuzz.GetOutput(number);
        
        Assert.That(result, Is.EqualTo("Buzz"));
    }

    [Test]
    public void GetOutput_NumberIsNotMultipleOf3And5_ReturnTheSameNumber()
    {
        int number = 4;

        string result = FizzBuzz.GetOutput(number);
        
        Assert.That(result, Is.EqualTo("4"));
    }
}