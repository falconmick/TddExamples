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

    [Test]
    public async Task HandleAsync_Should_Return_CarInfoUpdateResult()
    {
        var result = await _handler.HandleAsync(A.Fake<CarInfo>());
        
        Assert.NotNull(result);
    }
}

public record CarInfo();

public record CarInfoUpdateResult();

public class UpdateCarInfoCommandHandler : IUpdateCarInfoCommandHandler
{
    public async Task<CarInfoUpdateResult> HandleAsync(CarInfo fake)
    {
        return new CarInfoUpdateResult();
    }
}

public interface IUpdateCarInfoCommandHandler
{
    Task<CarInfoUpdateResult> HandleAsync(CarInfo fake);
}