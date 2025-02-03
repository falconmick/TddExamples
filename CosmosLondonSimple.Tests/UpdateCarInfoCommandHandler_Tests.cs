using FakeItEasy;

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
    public async Task HandleAsync_Should_Exist_And_Accept_CarInfo()
    {
        await _handler.HandleAsync(A.Fake<CarInfo>());
        Assert.Pass();
    }
}

public record CarInfo();

public class UpdateCarInfoCommandHandler : IUpdateCarInfoCommandHandler
{
    public Task HandleAsync(CarInfo fake)
    {
        return Task.CompletedTask;
    }
}

public interface IUpdateCarInfoCommandHandler
{
    Task HandleAsync(CarInfo fake);
}