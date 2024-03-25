// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using PathfinderFx.Integration;
using PathfinderFx.Integration.Model;

namespace SustainabilityCloudClient;

public static class SustainabilityClient
{
    public static void Main(string[] args)
    {
        //get config from appsettings.json
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        DataverseConfig dataverseConfig;
        DataLakeConfig fabricConfig;
        CosmosConfig cosmosConfig;
        try
        {
            dataverseConfig = new DataverseConfig()
            {
                Url = config["Url"],
                UserName = config["UserName"],
                Password = config["Password"]
            };

            fabricConfig = new DataLakeConfig()
            {
                DataLakeAccountName = config["DataLakeAccountName"],
                FileSystemName = config["FileSystemName"]
            };

            cosmosConfig = new CosmosConfig()
            {
                AccountEndpoint = config["AccountEndpoint"],
                CosmosDbName = config["CosmosDbName"],
                AuthKey = config["AuthKey"]
            };
        }
        catch (Exception e)
        {
            Console.WriteLine("Error reading configuration: " + e.Message);
            return;
        }

        //build a switch on cases of args using --s for setup, --c for clean, --i for integrate and a default returning an instruction to the user
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide an argument: --s for setup, --c for clean, --i for integrate, --a for list of available hosts, --u 'host' to specify a host and integrate with it");
            return;
        }

        ProductFootprintIntegrator pfIntegrator;
        switch (args[0])
        {
            case "--s":
                pfIntegrator = new ProductFootprintIntegrator(null, dataverseConfig, fabricConfig, cosmosConfig, true);
                var result = pfIntegrator.CreatePathfinderConfiguration();
                Console.WriteLine(result);
                break;
            case "--c":
                pfIntegrator = new ProductFootprintIntegrator(null, dataverseConfig, fabricConfig, cosmosConfig, false);
                var cleanResult = pfIntegrator.CleanDataverseTables();
                Console.WriteLine(cleanResult.Result);
                break;
            case "--i":
                pfIntegrator = new ProductFootprintIntegrator(null, dataverseConfig, fabricConfig, cosmosConfig, false);
                var integrationResult = pfIntegrator.IntegrateProductFootprints(false, "").Result;
                Console.WriteLine(integrationResult.ToJson());
                break;
            case "--a":
                pfIntegrator = new ProductFootprintIntegrator(null, dataverseConfig, fabricConfig, cosmosConfig, false);
                var hosts = pfIntegrator.GetPathfinderHosts();
                Console.WriteLine("Available hosts: ");
                foreach(var host in hosts)
                {
                    Console.WriteLine(host);
                }
                break;
            case "--u":
                //check to see if args[1] is not null
                if (args.Length < 2)
                {
                    Console.WriteLine("Please provide a host to integrate with: --a 'hostname'");
                }
                pfIntegrator = new ProductFootprintIntegrator(null, dataverseConfig, fabricConfig, cosmosConfig, false);
                var hostResult = pfIntegrator.SetCurrentPathfinderHost(args[1]);
                if (hostResult == "Host not found")
                {
                    Console.WriteLine(hostResult);
                }
                else
                {
                    Console.WriteLine(hostResult);
                    var integrationResult2 = pfIntegrator.IntegrateProductFootprints(false, "").Result;
                    Console.WriteLine(integrationResult2.ToJson());
                }
                break;
            case "--h":
                Console.WriteLine("Please provide an argument: --s for setup, --c for clean, --i for integrate, --a for list of available hosts, --u 'host' to specify a host and integrate with it");
                break;
            default:
                Console.WriteLine("Please provide an argument: --s for setup, --c for clean, --i for integrate, --a for list of available hosts, --u 'host' to specify a host and integrate with it");
                break;
        }
        

    }
}