# Cloud for Sustainability Pathfinder Client sample application

The Pathfinder Client is a dotnet core 8 console application that can be run on any platform that supports dotnet core 8. The client performs the following operations:

- Connects to Sustainability Manager and creates configuration tables for defining Pathfinder hosts to request Product Footprints from.
- Connects to one or more of the configured Pathfinder hosts and requests Product Footprints and can store them in:
  - Sustainability Manager's Data Model tables to be used in the Sustainability Manager's reporting and analytics.
  - Sustainability data solutions for Microsoft Fabric to be used in reporting, analytics and AI/ML models, via DataLake or CosmosDB and notebooks in Fabric.
