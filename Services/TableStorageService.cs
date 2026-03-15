using Azure;
using Azure.Data.Tables;
using ABCRetailCloud.Models;

namespace ABCRetailCloud.Services;

public class TableStorageService
{
    private readonly TableClient customerTable;
    private readonly TableClient productTable;

    public TableStorageService(IConfiguration config)
    {
        string conn = config.GetConnectionString("AzureStorage");

        var service = new TableServiceClient(conn);

        customerTable = service.GetTableClient("customers");
        productTable = service.GetTableClient("products");

        customerTable.CreateIfNotExists();
        productTable.CreateIfNotExists();
    }

    // ---------------- CUSTOMERS ----------------

    public async Task AddCustomer(Customer c)
    {
        await customerTable.AddEntityAsync(c);
    }

    public IEnumerable<Customer> GetCustomers()
    {
        return customerTable.Query<Customer>();
    }

    public async Task UpdateCustomer(Customer c)
    {
        await customerTable.UpdateEntityAsync(c, ETag.All);
    }

    public async Task DeleteCustomer(string id)
    {
        await customerTable.DeleteEntityAsync("CUSTOMER", id, ETag.All);
    }

    // ---------------- PRODUCTS ----------------

    public async Task AddProduct(Product p)
    {
        await productTable.AddEntityAsync(p);
    }

    public IEnumerable<Product> GetProducts()
    {
        return productTable.Query<Product>();
    }

    public async Task UpdateProduct(Product p)
    {
        await productTable.UpdateEntityAsync(p, ETag.All);
    }

    public async Task DeleteProduct(string id)
    {
        await productTable.DeleteEntityAsync("PRODUCT", id, ETag.All);
    }

    // ---------------- DEMO DATA ----------------

    public async Task GenerateDemoData()
    {
        var customers = new List<Customer>
    {
        new Customer { PartitionKey="CUSTOMER", RowKey="1", Name="John Doe", Email="john@mail.com" },
        new Customer { PartitionKey="CUSTOMER", RowKey="2", Name="Maria Silva", Email="maria@mail.com" },
        new Customer { PartitionKey="CUSTOMER", RowKey="3", Name="Pedro Santos", Email="pedro@mail.com" },
        new Customer { PartitionKey="CUSTOMER", RowKey="4", Name="Ana Costa", Email="ana@mail.com" },
        new Customer { PartitionKey="CUSTOMER", RowKey="5", Name="Luis Rocha", Email="luis@mail.com" }
    };

        foreach (var c in customers)
        {
            await customerTable.UpsertEntityAsync(c);
        }

        var products = new List<Product>
    {
        new Product { PartitionKey="PRODUCT", RowKey="1", Name="Laptop", Price=1200 },
        new Product { PartitionKey="PRODUCT", RowKey="2", Name="Phone", Price=800 },
        new Product { PartitionKey="PRODUCT", RowKey="3", Name="Tablet", Price=500 },
        new Product { PartitionKey="PRODUCT", RowKey="4", Name="Keyboard", Price=100 },
        new Product { PartitionKey="PRODUCT", RowKey="5", Name="Mouse", Price=50 }
    };

        foreach (var p in products)
        {
            await productTable.UpsertEntityAsync(p);
        }
    }
}