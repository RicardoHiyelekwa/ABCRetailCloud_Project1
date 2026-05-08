using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace AzureFunctions;

public class QueueStorageFunction
{
    [Function("QueueFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

        var queue = new QueueClient(connectionString, "ordersqueue");

        await queue.CreateIfNotExistsAsync();

        await queue.SendMessageAsync("New order processed");

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync("Queue message sent successfully");
        return response;
    }
}
