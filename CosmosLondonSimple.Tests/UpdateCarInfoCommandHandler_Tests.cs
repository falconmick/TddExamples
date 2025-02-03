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
        
        _handler = new UpdateCarInfoCommandHandler(_validator);
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

public interface ICarInfoValidator
{
    bool Validate(CarInfo carInfo);
}

public record CarInfo();

public record CarInfoUpdateResult(bool Successful);

public class UpdateCarInfoCommandHandler : IUpdateCarInfoCommandHandler
{
    private readonly ICarInfoValidator _validator;

    public UpdateCarInfoCommandHandler(ICarInfoValidator validator)
    {
        _validator = validator;
    }

    public async Task<CarInfoUpdateResult> HandleAsync(CarInfo carInfo)
    {
        _validator.Validate(carInfo);
        return new CarInfoUpdateResult(true);
    }
}

public interface IUpdateCarInfoCommandHandler
{
    Task<CarInfoUpdateResult> HandleAsync(CarInfo fake);
}