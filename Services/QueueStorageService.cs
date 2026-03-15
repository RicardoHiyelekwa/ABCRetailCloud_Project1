using Azure.Storage.Queues;

namespace ABCRetailCloud.Services;

public class QueueStorageService
{
    private readonly QueueClient queue;

    public QueueStorageService(IConfiguration config)
    {
        string conn = config.GetConnectionString("AzureStorage");

        queue = new QueueClient(conn, "orders");

        queue.CreateIfNotExists();
    }

    public async Task Send(string message)
    {
        await queue.SendMessageAsync(message);
    }

    public IEnumerable<string> List()
    {
        var messages = queue.ReceiveMessages(10);

        return messages.Value.Select(m => m.Body.ToString());
    }

    public async Task GenerateDemoMessages()
    {
        await Send("Processing Order #1001");
        await Send("Processing Order #1002");
        await Send("Inventory Update for Product 10");
        await Send("Uploading product image image1.jpg");
        await Send("Processing Order #1003");
    }
}