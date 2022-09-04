using NUnit.Framework;
using TestNinja.Fundamentals;

namespace Ninja.UnitTests;

[TestFixture]
public class ReservationTests
{
    [Test]
    public void CanBeCancelledBy_UserIsAdmin_ReturnTrue()
    {
        var reservation = new Reservation();
        var admin = new User
        {
            IsAdmin = true
        };
        bool result = reservation.CanBeCancelledBy(admin);

        Assert.That(result, Is.True);
    }
    
    [Test]
    public void CanBeCancelledBy_UserIsOwner_ReturnTrue()
    {
        var customer = new User()
        {
            IsAdmin = false
        };
        var reservation = new Reservation
        {
            MadeBy = customer
        };
        
        bool result = reservation.CanBeCancelledBy(customer);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void CanBeCancelledBy_UnknownUser_ReturnFalse()
    {
        var reservation = new Reservation
        {
            MadeBy = new User
            {
                IsAdmin = false
            }
        };
        
        bool result = reservation.CanBeCancelledBy(new User());
        
        Assert.That(result, Is.False);
    }
    
    
    

}