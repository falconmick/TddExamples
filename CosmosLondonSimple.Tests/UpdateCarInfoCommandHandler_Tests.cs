using FakeItEasy;

namespace CosmosLondonSimple.Tests;

public class UpdateCarInfoCommandHandler_Tests
{
    private ICarInfoValidator _validator;
    private IUpdateCarInfoCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _validator = A.Fake<ICarInfoValidator>();
        
        _handler = new UpdateCarInfoCommandHandler();
    }
    
    [Test]
    public async Task HandleAsync_Should_Accept_GoodInfo_And_Return_Success_Result()
    {
        var result = await _handler.HandleAsync(GoodCarInfo());
        
        Assert.True(result.Successful);
    }

    [Test]
    public async Task HandleAsync_Should_Validate_CarInfo()
    {
        var carInfo = GoodCarInfo();
        
        await _handler.HandleAsync(carInfo);

        A.CallTo(() => _validator.Validate(carInfo)).MustHaveHappenedOnceExactly();
    }

    private static CarInfo GoodCarInfo()
    {
        return A.Fake<CarInfo>();
    }
}

public record CarInfo();

public record CarInfoUpdateResult(bool Successful);

public class UpdateCarInfoCommandHandler : IUpdateCarInfoCommandHandler
{
    public async Task<CarInfoUpdateResult> HandleAsync(CarInfo fake)
    {
        return new CarInfoUpdateResult(true);
    }
}

public interface IUpdateCarInfoCommandHandler
{
    Task<CarInfoUpdateResult> HandleAsync(CarInfo fake);
}