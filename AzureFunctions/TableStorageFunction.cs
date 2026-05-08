using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace AzureFunctions;

public class TableStorageFunction
{
    [Function("StoreCustomerFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        var tableService = new TableServiceClient(connectionString);
        var table = tableService.GetTableClient("Customers");

        await table.CreateIfNotExistsAsync();

        var entity = new TableEntity("Customer", Guid.NewGuid().ToString())
        {
            { "FullName", "ABC Retail Customer" },
            { "Email", "customer@abcretail.com" }
        };

        await table.AddEntityAsync(entity);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync("Customer stored successfully");
        return response;
    }
}
