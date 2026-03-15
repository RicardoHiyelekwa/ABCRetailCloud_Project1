
using Microsoft.AspNetCore.Mvc;
using ABCRetailCloud.Services;

namespace ABCRetailCloud.Controllers;

public class FilesController:Controller
{
FileStorageService files;

public FilesController(FileStorageService f){files=f;}

public IActionResult Index()
{
return View(files.List());
}

[HttpPost]
public async Task<IActionResult> Upload(IFormFile file)
{
using var stream=file.OpenReadStream();
await files.Upload(file.FileName,stream);
return RedirectToAction("Index");
}

    public async Task<IActionResult> Generate()
    {
        await files.GenerateDemoFiles();
        return RedirectToAction("Index");
    }
}
