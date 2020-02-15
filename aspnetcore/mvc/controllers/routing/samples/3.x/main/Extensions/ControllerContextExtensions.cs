using Microsoft.AspNetCore.Mvc;

#region snippet
internal static class ControllerContextExtensions
{
    public static IActionResult ToActionResult(this ControllerContext ctx, int ?id, string msg = null) =>
        ctx.ToActionResult(id?.ToString(), msg);

    public static IActionResult ToActionResult(this ControllerContext ctx,
                                               string id = null, string msg = null)
    {
        var actionDescriptor = ctx.ActionDescriptor;
        var routeTemplate = actionDescriptor?.AttributeRouteInfo?.Template;
        var actionName = actionDescriptor.ActionName;
        var controllerName = actionDescriptor.ControllerName;
        var routeOrder = actionDescriptor.AttributeRouteInfo?.Order;
        var method = ctx.HttpContext.Request.Method;

        var tms = (routeTemplate == null) ? "" : $"Template = {routeTemplate}";
        var ids = (string.IsNullOrEmpty(id)) ? "" : $"id = {id}";
        var ors = (routeOrder == null) ? "" : $"Order = {routeOrder}";
        var methods = (method == "GET") ? "" : $"{method}";

        return new ContentResult { 
            Content = $"{methods} {ids} {ors} {tms} {controllerName}.{actionName} {msg}" };
    }
}
#endregion