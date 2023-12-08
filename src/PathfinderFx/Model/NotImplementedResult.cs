using Microsoft.AspNetCore.Mvc;

namespace PathfinderFx.Model;

public class NotImplementedResult : ActionResult
{
    private string GrantType { get; set; }

    public NotImplementedResult(string grantType)
    {
        GrantType = grantType;
    }
    
    public override void ExecuteResult(ActionContext context)
    {
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = 400;
        context.HttpContext.Response.WriteAsync($"{GrantType}");
    }
}