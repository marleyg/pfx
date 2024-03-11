using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using OpenIddict.Validation.AspNetCore;
using PathfinderFx.Config;
using PathfinderFx.Model;
using PathfinderFx.Providers;
using Quartz;

namespace PathfinderFx;

public static class Program
{
    internal static IPfxConfig? PfxConfig { get; private set; }
    private static bool UseAzureKeyVault { get; set; }
    public static void Main(string[] args)
    {
        //load the config from appsettings.json
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
            
        //check if the config is null and if not if the UseAzureKeyVault is set to false
        if (config["UseAzureKeyVault"] == "false")
        {
            UseAzureKeyVault = false;
            PfxConfig = config.GetSection("PfxConfig").Get<PfxConfig>();
            if (PfxConfig != null)
                Console.WriteLine(
                    "PfxConfig is set with {0} accounts from local appsettings.json, proceeding with the application",
                    PfxConfig.PathfinderAccounts.Count);
        }
        else
        {
            UseAzureKeyVault = true;
            PfxConfig = AksConfigurationProvider.PfxConfig 
                        ?? throw new InvalidOperationException("PfxConfig is null");
            Console.WriteLine("PfxConfig is set with {0} accounts, proceeding with the application", PfxConfig.PathfinderAccounts.Count);
        }
            
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        //builder.Services.AddControllersWithViews();
        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            });
        });
            
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            // Configure the context to use sqlite.
            options.UseSqlite($"Filename={Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "openiddict-aridka-server.sqlite3")}");

            // Register the entity sets needed by OpenIddict.
            // Note: use the generic overload if you need
            // to replace the default OpenIddict entities.
            options.UseOpenIddict();
        });

        // OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
        // (like pruning orphaned authorizations/tokens from the database) at regular intervals.
        builder.Services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });

        // Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
        builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        builder.Services.AddOpenIddict()

            // Register the OpenIddict core components.
            .AddCore(options =>
            {
                // Configure OpenIddict to use the Entity Framework Core stores and models.
                // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
                options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>();

                // Enable Quartz.NET integration.
                options.UseQuartz();
            })

            // Register the OpenIddict server components.
            .AddServer(options =>
            {
                // Enable the token endpoint.
                options.SetTokenEndpointUris("2/auth/token");
                    
                // Enable the client credentials flow.
                options.AllowClientCredentialsFlow();

                // Load and register the signing and encryption credentials from Azure Key Vault.
                if (UseAzureKeyVault){
                        
                    options.AddEncryptionCertificate(AksConfigurationProvider.GetEncryptionCertFromAks());
                    options.AddSigningCertificate(AksConfigurationProvider.GetSigningCertFromAks());
                }
                else
                {
                    //use ephemeral certs for development
                    options.AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();
                }

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough();
            })
                
            // Register the OpenIddict validation components.
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        });
        builder.Services.AddHostedService<Worker>();
            
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "Conformance 2.1.0",
                Title = "Pathfinder 2.1.0 API - Conformance 2.1.0",
                Description = "A Request/Response API for WBCSD:PACT Pathfinder 2.1.0 (Version 2.1.0-20231207) technical specifications, this is the v2.0 Data Model. This API is not intended for production use.",
                Contact = new OpenApiContact
                {
                    Name = "Marley Gray",
                    Email = "marleyg@microsoft.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });
            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
            
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapDefaultControllerRoute();

        app.Run();
    }
}