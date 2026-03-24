using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UCMS.Filters;

public class FooterFilter : IResultFilter
{
    private readonly ILogger<FooterFilter> _log;

    public FooterFilter(ILogger<FooterFilter> log)
    {
        _log = log;
    }

    public void OnResultExecuting(ResultExecutingContext ctx)
    {
        if (ctx.Controller is Controller c)
        {
            c.ViewData["FooterMessage"] = "Conestoga College - UCMS © 2026 - Confidential Academic System";
        }
        _log.LogInformation("Rendering view...");
    }

    public void OnResultExecuted(ResultExecutedContext ctx)
    {
        _log.LogInformation("View rendered at {T}", DateTime.Now.ToString("T"));
    }
}
