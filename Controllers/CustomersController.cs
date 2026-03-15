using Microsoft.AspNetCore.Mvc;
using ABCRetailCloud.Services;
using ABCRetailCloud.Models;

namespace ABCRetailCloud.Controllers;

public class CustomersController : Controller
{
    private readonly TableStorageService table;

    public CustomersController(TableStorageService t)
    {
        table = t;
    }

    public IActionResult Index()
    {
        return View(table.GetCustomers());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Customer c)
    {
        await table.AddCustomer(c);

        return RedirectToAction("Index");
    }

    public IActionResult Edit(string id)
    {
        var product = table.GetCustomers()
            .FirstOrDefault(x => x.RowKey == id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Customer c)
    {
        await table.UpdateCustomer(c);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        await table.DeleteCustomer(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> GenerateDemo()
    {
        await table.GenerateDemoData();
        return RedirectToAction("Index");
    }
}