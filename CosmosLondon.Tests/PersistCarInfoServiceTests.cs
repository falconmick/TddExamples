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
        _service.Insert();
        
        Assert.Pass();
    }
}

public class PersistCarInfoService : IPersistCarInfoService
{
}

internal interface IPersistCarInfoService
{
}