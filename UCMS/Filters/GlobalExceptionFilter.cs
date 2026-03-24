using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace UCMS.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _log;
    private readonly ITempDataDictionaryFactory _tmpFactory;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> log, ITempDataDictionaryFactory tmpFactory)
    {
        _log = log;
        _tmpFactory = tmpFactory;
    }

    public void OnException(ExceptionContext ctx)
    {
        _log.LogError(ctx.Exception, "Unhandled error caught by filter");

        var tmp = _tmpFactory.GetTempData(ctx.HttpContext);
        tmp["ErrorMessage"] = "Something went wrong while processing your request. Please try again later.";
        tmp.Keep("ErrorMessage");

        ctx.Result = new RedirectToActionResult("Error", "Home", null);
        ctx.ExceptionHandled = true;
    }
}
