using Microsoft.AspNetCore.Mvc;

namespace PathfinderFx.Model;

public class NotImplementedResult : ActionResult
{
    public override void ExecuteResult(ActionContext context)
    {
        context.HttpContext.Response.StatusCode = 400;
        context.HttpContext.Response.WriteAsync("Not implemented");
    }
}