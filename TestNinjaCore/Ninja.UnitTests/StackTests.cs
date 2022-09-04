using NUnit.Framework;

namespace Ninja.UnitTests;

[TestFixture]
public class StackTests
{
    private TestNinja.Fundamentals.Stack<string> _stack;

    [SetUp]
    public void SetUp()
    {
        _stack = new();
    }

    [Test]
    public void Count_EmptyStack_ReturnZero()
    {
        Assert.That(_stack.Count, Is.EqualTo(0));
    }

    [Test]
    public void Push_InputIsNull_ThrowArgumentNullException()
    {
        Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Push_StackWithFewItems_AddInputToTheStack()
    {
        _stack.Push("a");
        _stack.Push("b");
        
        Assert.That(_stack.Count, Is.EqualTo(2));
    }

    [Test]
    public void Pop_EmptyStack_ThrowInvalidOperationException()
    {
        Assert.That(() => { _stack.Pop();}, Throws.InvalidOperationException);
    }

    [Test]
    public void Pop_StackWithFewItems_ReturnTopItem()
    {
        _stack.Push("a");
        _stack.Push("b");

        string poppedItem = _stack.Pop();
        
        Assert.That(poppedItem, Is.EqualTo("b"));
    }
    
    [Test]
    public void Pop_StackWithFewItems_RemoveTopItem()
    {
        _stack.Push("a");
        _stack.Push("b");

        _stack.Pop();
        
        Assert.That(_stack.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void Peek_StackIsEmpty_ThrowInvalidOperationException()
    {
        Assert.That(() => { _stack.Peek();}, Throws.InvalidOperationException);
    }

    [Test]
    public void Peek_WhenCalled_ReturnTopItem()
    {
        _stack.Push("a");
        _stack.Push("b");

        string peekedItem = _stack.Peek();
        
        Assert.That(peekedItem, Is.EqualTo("b"));
    }

    [Test]
    public void Peek_StackWithFewItems_DoesNotRemoveTopItem()
    {
        _stack.Push("a");
        _stack.Push("b");

        _stack.Peek();
        
        Assert.That(_stack.Count, Is.EqualTo(2));
    }
    
    
}