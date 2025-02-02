using Microsoft.Azure.Cosmos;

namespace CosmosLondon.Adapters.Microsoft.Azure.Cosmos;

public class DatabaseAdapter(Database database)
{
    IContainer GetContainer(string id) => new ContainerAdapter(database.GetContainer(id));
}

public class ContainerAdapter(Container container) : IContainer
{
    public async Task<IItemResponse<T>> CreateItemAsync<T>(
        T item, 
        ItemRequestOptions requestOptions = null,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var result = await container.CreateItemAsync(
            item, 
            requestOptions: requestOptions, 
            cancellationToken: cancellationToken);

        return new ItemResponseAdapter<T>(result);
    }
}

public interface IContainer
{
    Task<IItemResponse<T>> CreateItemAsync<T>(
        T item,
        /*PartitionKey? partitionKey = null,*/ // note we have not implemented this as our code does not utilise
        ItemRequestOptions? requestOptions = null,
        CancellationToken cancellationToken = default);
}

public interface IItemResponse<T>
{
    
}

public class ItemResponseAdapter<T>(ItemResponse<T> result) : IItemResponse<T>
{
}