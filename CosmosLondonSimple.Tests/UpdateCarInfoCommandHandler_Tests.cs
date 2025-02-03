using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace CosmosLondonSimple.Tests;

public class UpdateCarInfoCommandHandler_Tests
{
    private ICarInfoValidator _validator;
    private IUpdateCarInfoCommandHandler _handler;
    private FakeLogger _logger;

    [SetUp]
    public void Setup()
    {
        _validator = A.Fake<ICarInfoValidator>();
        _logger = new FakeLogger();
        
        _handler = new UpdateCarInfoCommandHandler(_validator, _logger);
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
        
        Assert.That(_logger.CallList[0].LogLevel, Is.EqualTo(LogLevel.Error));
        Assert.That(_logger.CallList[0].Message, Is.EqualTo($"Car: {carName} invalid"));
    }

    private static CarInfo GoodCarInfo()
    {
        return A.Fake<CarInfo>();
    }
}

public class FakeLogger : ILogger
{
    public List<(LogLevel LogLevel, string Message)> CallList = new(); 
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        CallList.Add((logLevel, formatter(state, exception)));
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        // code here tracks scope with disposables

        return default;
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
    private readonly ILogger _logger;

    public UpdateCarInfoCommandHandler(ICarInfoValidator validator, ILogger logger)
    {
        _validator = validator;
        _logger = logger;
    }

    public async Task<CarInfoUpdateResult> HandleAsync(CarInfo carInfo)
    {
        var isValid = _validator.Validate(carInfo);

        if (!isValid)
        {
            _logger.LogError("Car: {CarName} invalid", carInfo.CarName);
        }
        
        return new CarInfoUpdateResult(isValid);
    }
}

public interface IUpdateCarInfoCommandHandler
{
    Task<CarInfoUpdateResult> HandleAsync(CarInfo fake);
}