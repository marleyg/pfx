using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using PathfinderFx.Integration.Model;

namespace PathfinderFx.Integration.Clients;

public class DataverseClient(ILoggerFactory loggerFactory, IDataverseConfig config)
{
    private IOrganizationService _orgService = new ServiceClient(config.ConnectionString);
    private readonly ILogger _logger = loggerFactory.CreateLogger<DataverseClient>();
    
    
}