using Azure.Storage.Files.Shares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace AzureFunctions;

public class FileStorageFunction
{
    [Function("AzureFileFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

        var share = new ShareClient(connectionString, "logs");
        await share.CreateIfNotExistsAsync();

        var root = share.GetRootDirectoryClient();
        var file = root.GetFileClient("log.txt");

        var content = "Azure File Storage Log";
        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));

        await file.CreateAsync(stream.Length);
        await file.UploadAsync(stream);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync("Azure File uploaded successfully");
        return response;
    }
}
