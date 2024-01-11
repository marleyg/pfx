using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using PathfinderFx.Integration.Model;

namespace PathfinderFx.Integration.Clients;

public class DataverseClient(ILoggerFactory loggerFactory, IDataverseConfig config)
{
    private IOrganizationService _orgService = new ServiceClient(config.ConnectionString);
    private readonly ILogger _logger = loggerFactory.CreateLogger<DataverseClient>();
    
    public string WhoAmI()
    {
        _logger.LogInformation("WhoAmI called");
        var response = (WhoAmIResponse)_orgService.Execute(new WhoAmIRequest());
        return response.UserId.ToString();
    }
}