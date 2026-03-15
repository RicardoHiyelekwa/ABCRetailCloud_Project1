
using Microsoft.AspNetCore.Mvc;

namespace ABCRetailCloud.Controllers;

public class HomeController:Controller
{
public IActionResult Index()
{
return View();
}
}
