using Moq;
using NUnit.Framework;
using TestNinjaCore.Mocking.MockingOrderService;

namespace Ninja.UnitTests.OrderServiceTests;

[TestFixture]
public class OrderServiceTests
{
    [Test]
    public void PlaceOrder_WhenCalled_StoreTheOrder()
    {
        var storage = new Mock<IStorage>();
        var orderService = new OrderService(storage.Object);
        var order = new Order();
        
        orderService.PlaceOrder(order);
        
        storage.Verify(s => s.Store(order));
    }
}