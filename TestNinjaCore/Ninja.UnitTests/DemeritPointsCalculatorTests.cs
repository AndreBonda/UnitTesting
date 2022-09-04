using NUnit.Framework;
using TestNinja.Fundamentals;

namespace Ninja.UnitTests;

[TestFixture]
public class DemeritPointsCalculatorTests
{
    private DemeritPointsCalculator _calculator;
    
    [SetUp]
    public void SetUp()
    {
        _calculator = new DemeritPointsCalculator();
    }

    [TestCase(-1)]
    [TestCase(301)]
    public void CalculateDemeritPoints_SpeedOutOfRange_ThrowArgumentOutOfRangeException(int speed)
    {
        Assert.That(() => _calculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(0)]
    [TestCase(5)]
    [TestCase(65)]
    public void CalculateDemeritPoints_SpeedLowerOrEqualToSpeedLimit_ReturnZeroPointsDemerit(int speed)
    {
        int demerit_pts = _calculator.CalculateDemeritPoints(speed);
        
        Assert.That(demerit_pts, Is.EqualTo(0));
    }

    [TestCase(66, 0)]
    [TestCase(70, 1)]
    [TestCase(75, 2)]
    public void CalculateDemeritPoints_SpeedGreaterThanSpeedLimit_ReturnCorrectAmountOfDemeritPoints(int speed, int expectedDemeritPts)
    {
        int demerit_pts = _calculator.CalculateDemeritPoints(speed);
        
        Assert.That(demerit_pts, Is.EqualTo(expectedDemeritPts));
    }
    
    

}