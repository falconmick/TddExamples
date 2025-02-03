using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace CosmosLondonSimple.Tests;

public class UpdateCarInfoCommandHandler_Tests
{
    private ICarInfoValidator _validator;
    private IUpdateCarInfoCommandHandler _handler;
    private ILogger _logger;

    [SetUp]
    public void Setup()
    {
        _validator = A.Fake<ICarInfoValidator>();
        
        _handler = new UpdateCarInfoCommandHandler(_validator);
    }
    
    [Test]
    public async Task HandleAsync_Should_Accept_GoodInfo_And_Return_Success_Result()
    {
        A.CallTo(() => _validator.Validate(A<CarInfo>._)).Returns(true);
        
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

    [Test]
    public async Task HandleAsync_Should_Return_Non_Success_When_Validation_Failed()
    {
        var carInfo = GoodCarInfo();
        A.CallTo(() => _validator.Validate(carInfo)).Returns(false);

        var result = await _handler.HandleAsync(carInfo);
        
        Assert.False(result.Successful);
    }

    [Test]
    public async Task HandleAsync_Should_Log_Error_If_Validation_Failed()
    {
        var carName = "Ford";
        var carInfo = GoodCarInfo() with
        {
            CarName = carName
        };
        A.CallTo(() => _validator.Validate(carInfo)).Returns(false);
        
        await _handler.HandleAsync(carInfo);
        
        A.CallTo(() => _logger.LogError("Car: {CarName} invalid", carName)).MustHaveHappenedOnceExactly();
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

public record CarInfo(string CarName);

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
        return new CarInfoUpdateResult(_validator.Validate(carInfo));
    }
}

public interface IUpdateCarInfoCommandHandler
{
    Task<CarInfoUpdateResult> HandleAsync(CarInfo fake);
}