using Azure.Storage.Blobs;

namespace ABCRetailCloud.Services;

public class BlobStorageService
{
    private readonly BlobContainerClient container;

    public BlobStorageService(IConfiguration config)
    {
        string conn = config.GetConnectionString("AzureStorage");

        container = new BlobContainerClient(conn, "images");

        container.CreateIfNotExists();
    }

    public async Task Upload(string name, Stream data)
    {
        var blob = container.GetBlobClient(name);

        await blob.UploadAsync(data, true);
    }

    public IEnumerable<string> List()
    {
        return container.GetBlobs().Select(x => x.Name);
    }

    public string GetUrl(string name)
    {
        return container.GetBlobClient(name).Uri.ToString();
    }
}