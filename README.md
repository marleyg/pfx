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

The PathfinderFx Host is a dotnet core 8 web application that can be run on any platform that supports dotnet core 8. The host provides the WBCSD:PACT conformant API for the Pathfinder Framework and sample Product Footprint data using the [2.1 specification](https://wbcsd.github.io/data-exchange-protocol/v2/). The host also provides a sample client application that integrates Pathfinder Product Footprints with the [Cloud for Sustainability](https://www.microsoft.com/en-us/sustainability/cloud).

The host can be run locally or deployed to any cloud platform that supports dotnet core 8. The following instructions are for running the host locally:

1. Install [dotnet core 8](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Clone the PathfinderFx repository
3. Open a command prompt and navigate to the PathfinderFx\src\PathfinderFx folder
4. Check the [appsettings.config](src/PathfinderFx/appsettings.json) file and update the `UseAzureKeyVault` to `false` and update the `PfxConfig` section with the appropriate values for your accounts you would like to test with.  Note, an account must have the `api` permission to be able to access the API.

    ```json
    {
            "ClientId": "ctest",
            "ClientSecret": "some_secret",
            "DisplayName": "Conformance test application 2",
            "Permissions": [
            "api"
            ]
        }
    ```

5. Once you have the settings updated, you can run the host by building then running the application using the following commands from the `src/PathfinderFx` folder:

    ```bash

    dotnet build
    dotnet run

    ```

6. Using a tool like [Postman](https://www.postman.com/) or [Insomnia](https://insomnia.rest/), you can now access the API. The following steps will show you how to obtain an access token and use it to call the API.

7. You will 1st need to obtain an access token from the host. To do this, send a POST request to the following URL: https://localhost:5001/2/auth/token. The request should include the following form data:

    - grant_type: client_credentials
    - client_id: "see the appsettings.json file for the value for an account to use"
    - client_secret: "see the appsettings.json file for the value for an secret to use for the account"

8. The response will include an access_token that can be used to call the API. Copy the access_token value and use it in the Authorization header of subsequent requests to the API. For example, the following request will return a list of all the Product Footprints in the host: https://localhost:5001/2/footprints. The request should include the following header:

    - Authorization: Bearer "access_token value from step 7"

To set this API up in an Azure Tenant, see the [Azure Setup](./docs/azure-setup.md) documentation.

## Solution Sample - PathfinderFx.Integration

The PathfinderFx sample can be used to build a Pathfinder API implementation and also provides sample client application library that integrates Pathfinder Product Footprints with the [Cloud for Sustainability](https://www.microsoft.com/en-us/sustainability/cloud).

The sample includes the following projects:

- PathfinderFx.Integration: the client application library that has a WBCSD Pathfinder conformant client that can be used to request product footprints from other Pathfinder Hosts and then integrate these product footprints into [Sustainability Manager](https://www.microsoft.com/en-us/sustainability/microsoft-sustainability-manager) and to [Sustainability data solutions for Microsoft Fabric](https://learn.microsoft.com/en-us/industry/sustainability/sustainability-data-solutions-overview).
- IntegrationHarness: a console application that uses the `PathfinderFx.Integration` library to demonstrate how to use the library to request product footprints from a Pathfinder Host and then integrate these product footprints into [Sustainability Manager](https://www.microsoft.com/en-us/sustainability/microsoft-sustainability-manager) .
  - See the [IntegrationHarness/README.md](src/PathfinderFx.Integration/IntegrationHarness/README.md) for more information on how to use the IntegrationHarness.
