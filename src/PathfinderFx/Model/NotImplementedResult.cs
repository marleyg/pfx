using Microsoft.AspNetCore.Mvc;

namespace PathfinderFx.Model;

public class NotImplementedResult(string grantType) : ActionResult
{
    private string GrantType { get; set; } = grantType;

    public override void ExecuteResult(ActionContext context)
    {
        context.HttpContext.Response.StatusCode = 400;
        context.HttpContext.Response.WriteAsync($"{GrantType},not implemented");
    }
}