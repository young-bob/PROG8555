using Microsoft.AspNetCore.Mvc.Filters;

namespace UCMS.Filters;

public class ActivityLogFilter : IActionFilter
{
    private readonly ILogger<ActivityLogFilter> _log;

    public ActivityLogFilter(ILogger<ActivityLogFilter> log)
    {
        _log = log;
    }

    public void OnActionExecuting(ActionExecutingContext ctx)
    {
        var user = ctx.HttpContext.User.Identity?.IsAuthenticated == true
            ? ctx.HttpContext.User.Identity.Name
            : "Guest";

        var ctrl = ctx.RouteData.Values["controller"];
        var act = ctx.RouteData.Values["action"];
        var now = DateTime.Now;

        _log.LogInformation(">> Action started: {Ctrl}/{Act} by [{User}] at {Time}", ctrl, act, user, now.ToString("yyyy-MM-dd HH:mm:ss"));

        ctx.HttpContext.Items["_startedAt"] = now;
    }

    public void OnActionExecuted(ActionExecutedContext ctx)
    {
        if (ctx.HttpContext.Items.TryGetValue("_startedAt", out var obj) && obj is DateTime t0)
        {
            var elapsed = (DateTime.Now - t0).TotalMilliseconds;
            var ok = ctx.Exception == null;

            _log.LogInformation("<< Action finished in {Ms:F1}ms (success={Ok})", elapsed, ok);
        }
    }
}
