using Microsoft.AspNetCore.Mvc;
using UCMS.Filters;

namespace UCMS.Controllers;

[ServiceFilter(typeof(ActivityLogFilter))]
public class CourseController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Details()
    {
        return View();
    }
}
