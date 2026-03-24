using Microsoft.AspNetCore.Mvc;

namespace UCMS.Controllers;

public class StudentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Registration()
    {
        return View();
    }
}
