using Microsoft.AspNetCore.Mvc;
using ABCRetailCloud.Services;

namespace ABCRetailCloud.Controllers;

public class QueueController : Controller
{
    private readonly QueueStorageService queue;

    public QueueController(QueueStorageService q)
    {
        queue = q;
    }

    public IActionResult Index()
    {
        var messages = queue.List();
        return View(messages);
    }

    [HttpPost]
    public async Task<IActionResult> Send(string message)
    {
        await queue.Send(message);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Generate()
    {
        await queue.GenerateDemoMessages();
        return RedirectToAction("Index");
    }
}