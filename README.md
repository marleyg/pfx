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
5. Open tool like [Postman](https://www.postman.com/) and test the API using the [Pathfinder Api collection](./src//postman/Pathfinder%20APIs.postman_collection.json)using ehjter the localhost URL: <https://localhost:5001> or the Azure URL: <https://pathfinderfx.azurewebsites.net>.
6. You will 1st need to obtain an access token from the host. To do this, send a POST request to the following URL: <https://localhost:5001/2/auth/token>. The request should include the following form data:

- grant_type: client_credentials
- client_id: "see the appsettings.json file for the value for an account to use"
- client_secret: "see the appsettings.json file for the value for an secret to use for the account"

7.The response will include an access_token that can be used to call the API. Copy the access_token value and use it in the Authorization header of subsequent requests to the API. For example, the following request will return a list of all the Product Footprints in the host: <https://localhost:5001/2/footprints>. The request should include the following header:

- Authorization: Bearer "access_token value from step 6"

To set this API up in an Azure Tenant, see the [Azure Setup](./docs/azure-setup.md) documentation.

## Client Solution Samples

The PathfinderFx [Cloud for Sustainability](https://www.microsoft.com/en-us/sustainability/cloud) sample also provides an example Pathfinder Client that integrates with the Cloud for Sustainability [Data Model](https://docs.microsoft.com/en-us/sustainability/data-model/overview) for both [Sustainability Manager](https://www.microsoft.com/en-us/sustainability/microsoft-sustainability-manager) and [Sustainability data solutions ]. The client is a dotnet core 8 console application that can be run on any platform that supports dotnet core 8.

The client performs the following operations:

- Connects to Sustainability Manager and creates configuration tables for defining Pathfinder hosts to request Product Footprints from.
- Connects to one or more of the configured Pathfinder hosts and requests Product Footprints and can store them in:
- Sustainability Manager's Data Model tables to be used in the Sustainability Manager's reporting and analytics.
- Sustainability data solutions for Microsoft Fabric to be used in reporting, analytics and AI/ML models, via DataLake or CosmosDB and notebooks in Fabric.

Details for setting up and running the client can be found in the [Client Setup](./docs/client-setup.md) documentation.
