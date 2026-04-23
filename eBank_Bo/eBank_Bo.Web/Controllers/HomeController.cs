using Microsoft.AspNetCore.Mvc;

namespace eBank_Bo.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
