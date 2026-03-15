
using Microsoft.AspNetCore.Mvc;
using ABCRetailCloud.Services;

namespace ABCRetailCloud.Controllers;

public class BlobController:Controller
{
BlobStorageService blob;

public BlobController(BlobStorageService b){blob=b;}

public IActionResult Index()
{
ViewBag.Service=blob;
return View(blob.List());
}

    public string GetContentType(string name)
    {
        if (name.EndsWith(".png")) return "image/png";
        if (name.EndsWith(".jpg") || name.EndsWith(".jpeg")) return "image/jpeg";
        if (name.EndsWith(".gif")) return "image/gif";
        if (name.EndsWith(".webp")) return "image/webp";
        if (name.EndsWith(".pdf")) return "application/pdf";

        return "application/octet-stream";
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            await blob.Upload(file.FileName, stream);
        }

        return RedirectToAction("Index");
    }
}
