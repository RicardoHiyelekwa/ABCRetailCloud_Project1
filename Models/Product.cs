using Azure;
using Azure.Data.Tables;

namespace ABCRetailCloud.Models;

public class Product : ITableEntity
{
    public string PartitionKey { get; set; } = "PRODUCT";
    public string RowKey { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; }
    public double Price { get; set; }

    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}