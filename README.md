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

## Solution Samples

The PathfinderFx sample can be used to build a Pathfinder API implementation and also be used in integrated Solution Samples. The following Solution Samples are available:

- [Custom API Connector]() - A custom API connector to import data from a Pathfinder Host into Microsoft Power Platform and Dataverse applications like [Sustainability Manager](https://appsource.microsoft.com/en-us/product/dynamics-365/mscrm.6b0b9b9e-2b1e-4e9f-9d9c-2b9e8c4d8c6a?tab=Overview) and [Sustainability Reporting](https://appsource.microsoft.com/en-us/product/dynamics-365/mscrm.6b0b9b9e-2b1e-4e9f-9d9c-2b9e8c4d8c6a?tab=Overview).
- [Dataverse Custom API](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/custom-api) - a solution that can request Product Footprint data from many different Pathfinder Hosts and store the data in Dataverse.