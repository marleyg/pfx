using Microsoft.AspNetCore.Mvc;

namespace PathfinderFx.Config;

public class NotImplementedResult(string grantType) : ActionResult
{
    private string GrantType { get; set; } = grantType;

    public override void ExecuteResult(ActionContext context)
    {
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = 400;
        context.HttpContext.Response.WriteAsync($"{GrantType}");
    }
}