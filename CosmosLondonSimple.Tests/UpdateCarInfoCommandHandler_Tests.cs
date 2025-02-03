namespace CosmosLondonSimple.Tests;

public class UpdateCarInfoCommandHandler_Tests
{
    private IUpdateCarInfoCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _handler = new UpdateCarInfoCommandHandler();
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}

public class UpdateCarInfoCommandHandler : IUpdateCarInfoCommandHandler
{
}

public interface IUpdateCarInfoCommandHandler
{
}