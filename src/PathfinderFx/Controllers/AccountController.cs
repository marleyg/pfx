using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PathfinderFx.Model;
using PathfinderFx.ViewModels.Account;

namespace PathfinderFx.Controllers;

[Authorize]
public class AccountController(
    UserManager<ApplicationUser> userManager,
    ApplicationDbContext applicationDbContext)
    : Controller
{
    private static bool _databaseChecked;

    //
    // POST: /Account/Register
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        EnsureDatabaseCreated(applicationDbContext);
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await userManager.FindByNameAsync(model.Email);
        if (user != null)
        {
            return StatusCode(StatusCodes.Status409Conflict);
        }

        user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return Ok();
        }
        AddErrors(result);

        // If we got this far, something failed.
        return BadRequest(ModelState);
    }

    #region Helpers

    // The following code creates the database and schema if they don't exist.
    // This is a temporary workaround since deploying database through EF migrations is
    // not yet supported in this release.
    // Please see this http://go.microsoft.com/fwlink/?LinkID=615859 for more information on how to do deploy the database
    // when publishing your application.
    private static void EnsureDatabaseCreated(ApplicationDbContext context)
    {
        if (!_databaseChecked)
        {
            _databaseChecked = true;
            context.Database.EnsureCreated();
        }
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    #endregion
}