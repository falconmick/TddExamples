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

    // v3
    [Test]
    public void Insert_Should_Exist_Within_PersistCarInfoService()
    {
        _service.InsertAsync(A.Fake<CarInfo>());
        
        Assert.Pass();
    }
}

public class PersistCarInfoService : IPersistCarInfoService
{
    public async Task InsertAsync(CarInfo carInfo)
    {
        throw new NotImplementedException();
    }
}

internal interface IPersistCarInfoService
{
    Task InsertAsync(CarInfo carInfo);
}