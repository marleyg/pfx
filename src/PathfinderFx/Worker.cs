using OpenIddict.Abstractions;
using PathfinderFx.Model;

namespace PathfinderFx;

public class Worker(IServiceProvider serviceProvider)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var config = Program.PfxConfig;
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        
        //looping through the accounts in the config and creating them if they don't exist
        foreach (var account in config!.ConformanceAccounts)
        {
            if (await manager.FindByClientIdAsync(account.ClientId, cancellationToken) != null) continue;
            var descriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = account.ClientId,
                ClientSecret = account.ClientSecret,
                DisplayName = account.DisplayName,
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials
                }
            };
            if (account.Permissions.Contains("api"))
            {
                descriptor.Permissions.Add("api");
            }

            await manager.CreateAsync(descriptor, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}