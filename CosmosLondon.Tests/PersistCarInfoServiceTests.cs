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
    public void Insert_Should_Exist_Within_PersistCarInfoService()
    {
        _service.InsertAsync(A.Fake<CarInfo>());
        
        Assert.Pass();
    }

    [Test]
    public async Task Insert_Should_Return_CarInfoResult()
    {
        var result = await _service.InsertAsync(A.Fake<CarInfo>());
        
        Assert.NotNull(result);
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