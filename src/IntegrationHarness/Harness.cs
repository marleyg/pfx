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
        
        var dataverseConfig = new DataverseConfig()
        {
            Url = config["Url"],
            UserName = config["UserName"],
            Password = config["Password"]
        };


        var pfIntegrator = new ProductFootprintIntegrator(null, dataverseConfig, false);
        
        //var result = pfIntegrator.CreatePathfinderConfiguration();
        //Console.WriteLine(result);
        
        var result = pfIntegrator.IntegrateProductFootprints(false).Result;
        Console.WriteLine("Footprints processed: " + result.RecordsProcessed);
        Console.WriteLine("Successful: " + result.Success);
        Console.WriteLine("Details: " + result.Message);
    }
}