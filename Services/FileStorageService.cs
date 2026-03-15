using Azure.Storage.Files.Shares;

namespace ABCRetailCloud.Services;

public class FileStorageService
{
    private readonly ShareClient share;

    public FileStorageService(IConfiguration config)
    {
        string conn = config.GetConnectionString("AzureStorage");

        share = new ShareClient(conn, "files");

        share.CreateIfNotExists();
    }

    public async Task Upload(string name, Stream data)
    {
        var dir = share.GetRootDirectoryClient();
        var file = dir.GetFileClient(name);

        await file.CreateAsync(data.Length);

        data.Position = 0;

        await file.UploadRangeAsync(
            new Azure.HttpRange(0, data.Length),
            data
        );
    }

    public IEnumerable<string> List()
    {
        var dir = share.GetRootDirectoryClient();

        return dir.GetFilesAndDirectories()
                  .Select(x => x.Name);
    }

    public async Task GenerateDemoFiles()
    {
        for (int i = 1; i <= 5; i++)
        {
            var content = new MemoryStream(
                System.Text.Encoding.UTF8.GetBytes($"Log file {i}")
            );

            await Upload($"log{i}.txt", content);
        }
    }
}