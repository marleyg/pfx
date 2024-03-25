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

        for (var i = 0; i < args.Length; i++)
        {
            ProductFootprintIntegrator pfIntegrator;
            switch (args[i])
            {
                case "--i":
                    pfIntegrator = new ProductFootprintIntegrator(null, dataverseConfig, true);
                    var initResult = pfIntegrator.CreatePathfinderConfiguration();
                    Console.WriteLine(initResult);
                    break;
                case "--c":
                    pfIntegrator = new ProductFootprintIntegrator(null, dataverseConfig, false);
                    var cleanResult = pfIntegrator.CleanDataverseTables();
                    Console.WriteLine(cleanResult.Result);
                    break;
                case "--h":
                    pfIntegrator = new ProductFootprintIntegrator(null, dataverseConfig, false);
                    var s = pfIntegrator.GetPathfinderHosts();
                    Console.WriteLine(s.Count);
                    foreach (var host in s)
                    {
                        Console.WriteLine(host);
                    }
                    break;
                case "--f":
                    if (i + 1 < args.Length)
                    {
                        var pathfinderHost = args[i + 1];

                        pfIntegrator = new ProductFootprintIntegrator(null, dataverseConfig, false);
                        var t = pfIntegrator.SetCurrentPathfinderHost(pathfinderHost);
                        Console.WriteLine(t);
                        Console.WriteLine("Calling {0} to integrate footprints", pathfinderHost);
                    
                        var result = pfIntegrator.IntegrateProductFootprints(false).Result;
                        Console.WriteLine("Footprints processed: " + result.RecordsProcessed);
                        Console.WriteLine("Successful: " + result.Success);
                        Console.WriteLine("Details: " + result.Message);
                    }
                    else
                    {
                        Console.WriteLine("No host provided");
                    }

                    break;
            }
        }
        
        Console.WriteLine("Usage: ");
        Console.WriteLine("--i: Initialize Pathfinder Configuration");
        Console.WriteLine("--c: Clean Dataverse Tables of Footprints created via testing");
        Console.WriteLine("--h: Get Configured Pathfinder Hosts");
        Console.WriteLine("--f: <HOST_NAME> Integrate Footprints from the specified host");

    }
}