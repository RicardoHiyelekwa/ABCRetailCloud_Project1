
using Microsoft.AspNetCore.Mvc;
using ABCRetailCloud.Services;
using ABCRetailCloud.Models;

namespace ABCRetailCloud.Controllers;

public class ProductsController : Controller
{
    TableStorageService table;

    public ProductsController(TableStorageService t) { table = t; }

    public IActionResult Index()
    {
        return View(table.GetProducts());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product c)
    {
        await table.AddProduct(c);

        return RedirectToAction("Index");
    }

    public IActionResult Edit(string id)
    {
        var product = table.GetProducts()
            .FirstOrDefault(x => x.RowKey == id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product c)
    {
        await table.UpdateProduct(c);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest();

        await table.DeleteProduct(id);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> GenerateDemo()
    {
        await table.GenerateDemoData();
        return RedirectToAction("Index");
    }
}