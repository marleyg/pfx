using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using PathfinderFx.Model;

namespace PathfinderFx;

public class Worker : IHostedService
{
    private readonly IOptions<PfxConfig> _config;
    private readonly IServiceProvider _serviceProvider;

    public Worker(IOptions<PfxConfig> config, IServiceProvider serviceProvider)
    {
        _config = config;
        _serviceProvider = serviceProvider;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        
        //looping through the accounts in the config and creating them if they don't exist
        foreach (var account in _config.Value.ConformanceAccounts)
        {
            if (await manager.FindByClientIdAsync(account.ClientId, cancellationToken) == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = account.ClientId,
                    ClientSecret = account.ClientSecret,
                    DisplayName = account.DisplayName,
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials
                    }
                }, cancellationToken);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}