# WBSCD Pathfinder Framework (FX)

The WBCSD Pathfinder Framework is a Microsoft based set of code samples and documentation for building a WBCSD PACT conformant Pathfinder application. Specifically, an implementation of the [2.1.0](https://wbcsd.github.io/tr/2023/data-exchange-protocol-20231207/) PCF Data Exchange protocol specification API and example client applications.

The code samples are built using the following Microsoft technologies:

- [dotnet core 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)
- [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
- [OpenIddict](https://documentation.openiddict.com)
- [Microsoft Entra Id](https://www.microsoft.com/en-us/security/business/microsoft-entra)
- [Microsoft Power Platform](https://powerplatform.microsoft.com/en-us/)

See the [FAQ](./docs/faq.md) for more information about the Pathfinder Framework and what conformance to PACT means for the Microsoft Cloud for Sustainability.

## Getting Started

The example `host` API implementation can be access via REST API at the following URL: https://pathfinderfx.azurewebsites.net.

### Standing up your own PathfinderFx Host

The PathfinderFx Host is a dotnet core 8 web application that can be run on any platform that supports dotnet core 8. The host provides the WBCSD:PACT conformant API for the Pathfinder Framework and sample Product Footprint data using the [2.1 specification](https://wbcsd.github.io/data-exchange-protocol/v2/). The host also provides a sample client application that can be used to test the API.

The host can be run locally or deployed to any cloud platform that supports dotnet core 8. The following instructions are for running the host locally:

1. Install [dotnet core 8](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Clone the PathfinderFx repository
3. Open a command prompt and navigate to the PathfinderFx\src\PathfinderFx folder
4. Run the following command to start the host: `dotnet build; dotnet run`
5. Open tool like [Postman](https://www.postman.com/) and test the API using the following URL: https://localhost:5001
6. You will 1st need to obtain an access token from the host. To do this, send a POST request to the following URL: https://localhost:5001/2/auth/token. The request should include the following form data:
 - grant_type: client_credentials
 - client_id: "see the appsettings.json file for the value for an account to use"
 - client_secret: "see the appsettings.json file for the value for an secret to use for the account"
7. The response will include an access_token that can be used to call the API. Copy the access_token value and use it in the Authorization header of subsequent requests to the API. For example, the following request will return a list of all the Product Footprints in the host: https://localhost:5001/2/footprints. The request should include the following header:
 - Authorization: Bearer "access_token value from step 6" 

To set this API up in an Azure Tenant, see the [Azure Setup](./docs/azure-setup.md) documentation.

## Solution Samples

The PathfinderFx sample can be used to build a Pathfinder API implementation and also be used in integrated Solution Samples. The following Solution Samples are available:

- [Custom API Connector]() - A custom API connector to import data from a Pathfinder Host into Microsoft Power Platform and Dataverse applications like [Sustainability Manager](https://appsource.microsoft.com/en-us/product/dynamics-365/mscrm.6b0b9b9e-2b1e-4e9f-9d9c-2b9e8c4d8c6a?tab=Overview) and [Sustainability Reporting](https://appsource.microsoft.com/en-us/product/dynamics-365/mscrm.6b0b9b9e-2b1e-4e9f-9d9c-2b9e8c4d8c6a?tab=Overview).
- [Dataverse Custom API](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/custom-api) - a solution that can request Product Footprint data from many different Pathfinder Hosts and store the data in Dataverse.