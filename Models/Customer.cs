using Azure;
using Azure.Data.Tables;

namespace ABCRetailCloud.Models;

public class Customer : ITableEntity
{
    public string PartitionKey { get; set; } = "CUSTOMER";
    public string RowKey { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; }
    public string Email { get; set; }

    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}