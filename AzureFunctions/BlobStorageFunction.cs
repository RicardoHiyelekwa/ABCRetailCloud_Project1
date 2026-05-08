using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace AzureFunctions;

public class BlobStorageFunction
{
    [Function("UploadBlobFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

        var blobService = new BlobServiceClient(connectionString);
        var container = blobService.GetBlobContainerClient("productimages");

        await container.CreateIfNotExistsAsync();

        var blob = container.GetBlobClient($"product-{Guid.NewGuid()}.txt");

        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("ABC Retail Blob File"));

        await blob.UploadAsync(stream);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync("Blob uploaded successfully");
        return response;
    }
}
