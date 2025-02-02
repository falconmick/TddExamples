using CosmosLondon.Models;
using FakeItEasy;

namespace CosmosLondon.Tests;

public class PersistCarInfoServiceTests
{
    private IPersistCarInfoService _service; // compile error

    [SetUp]
    public void Setup()
    {
        _service = new PersistCarInfoService(); // compile error
    }

    [Test]
    public async Task Insert_Should_Return_Success_CarInfoResult_When_Good_CarInfo()
    {
        var carInfo = GoodCarInfo();
        
        var result = await _service.InsertAsync(carInfo);
        
        Assert.NotNull(result);
    }

    private static CarInfo GoodCarInfo()
    {
        return A.Fake<CarInfo>();
    }
}

public class PersistCarInfoService : IPersistCarInfoService
{
    public async Task<CarInfoInsertResult> InsertAsync(CarInfo carInfo)
    {
        return new CarInfoInsertResult(true);
    }
}

// todo: maybe this alaways had existed from prior ticket
internal interface IPersistCarInfoService
{
    Task<CarInfoInsertResult> InsertAsync(CarInfo carInfo);
}