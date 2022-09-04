using NUnit.Framework;
using Math = TestNinja.Fundamentals.Math;

namespace Ninja.UnitTests;

[TestFixture]
public class MathTests
{
    private Math _math;
    
    [SetUp]
    public void SetUp()
    {
        _math = new Math();
    }

    [TestCase(0,0)]
    [TestCase(-2,3)]
    [TestCase(2,3)]
    [TestCase(-2,-3)]
    public void Add_WhenCalled_ReturnsTheSumOfArguments(int a, int b)
    {
        int sum = _math.Add(a,b);

        Assert.That(sum, Is.EqualTo(a + b));
    }

    [TestCase(1,2,2)]
    [TestCase(2,1,2)]
    [TestCase(1,1,1)]
    public void Max_WhenCalled_ReturnsGrater(int a, int b, int expected)
    {
        int max = _math.Max(a, b);
        
        Assert.That(max, Is.EqualTo(expected));
    }
    
    [TestCase(5, new int[]{1,3,5})]
    public void GetOddNumbers_LimitIsGreaterThanZero_ReturnsOddNumberUpToLimit(int limit, int[] expected)
    {
        var results = _math.GetOddNumbers(limit);
        
        Assert.That(results, Is.EquivalentTo(expected));
    }

    [TestCase(0, 0)]
    [TestCase(-3, 0)]
    public void GetOddNumbers_LimitIsEqualOrLowerToZero_ReturnEmptyCollection(int limit, int expectedCount)
    {
        var result = _math.GetOddNumbers(limit);
        
        Assert.That(result.Count(), Is.EqualTo(expectedCount));
    }

}