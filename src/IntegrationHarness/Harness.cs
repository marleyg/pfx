// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using PathfinderFx.Integration;
using PathfinderFx.Integration.Model;

namespace IntegrationHarness;

public static class Harness
{
    public static void Main(string[] args)
    {
        //get config from appsettings.json
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        
        var pathfinderConfig = new PathfinderConfig
        {
            HostUrl = config["HostUrl"],
            AuthUrl = config["AuthUrl"],
            ClientId = config["ClientId"],
            ClientSecret = config["ClientSecret"]
        };

        var pfIntegrator = new ProductFootprintIntegrator(pathfinderConfig);
        var result = pfIntegrator.IntegrateProductFootprints().Result;
        Console.WriteLine(result);
    }
}