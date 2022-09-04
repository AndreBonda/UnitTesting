using NUnit.Framework;
using TestNinja.Fundamentals;

namespace Ninja.UnitTests;

[TestFixture]
public class CustomerControllerTests
{
    [TestCase(0)]
    [TestCase(-1)]
    public void GetCustomer_UserIdNotExists_ReturnsNotFound(int id)
    {
        var controller = new CustomerController();

        var result = controller.GetCustomer(id);
        
        Assert.That(result, Is.TypeOf<NotFound>());
    }
    
    [Test]
    public void GetCustomer_UserIdExists_ReturnsOk()
    {
        var controller = new CustomerController();

        var result = controller.GetCustomer(1);
        
        Assert.That(result, Is.TypeOf<Ok>());
    }

}