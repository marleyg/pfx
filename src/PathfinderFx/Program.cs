using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OpenIddict.Server;
using PathfinderFx;
using PathfinderFx.Model;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Configure the context to use sqlite.
    options.UseSqlite($"Filename={Path.Combine(Path.GetTempPath(), "openiddict-aridka-server.sqlite3")}");

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

        options.AddEventHandler<OpenIddictServerEvents.ApplyTokenResponseContext>(b =>
            b.UseSingletonHandler<MyApplyTokenResponseHandler>());
        
        // Enable the client credentials flow.
        options.AllowClientCredentialsFlow();

        // Register the signing and encryption credentials.
        options.AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate();

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

builder.Services.AddHostedService<Worker>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1-Conformance 1.0.0",
        Title = "Pathfinder 2.1.0 API - Conformance 1.0.0",
        Description = "A Request/Response API for WBCSD:PACT Pathfinder 2.1.0 (Version 2.0.1-20230522) technical specifications, this is the v2.0 Data Model. This API is not intended for production use.",
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
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


//block of customized text responses to errors needed for conformance to the WBCSD:PACT Pathfinder 2.1.0 (Version 2.0.1-20230522) technical specifications, only works after OpenIddict validation is passed
app.Use(async (context, next) =>
{
    await next.Invoke();
    
    switch (context.Response.StatusCode)
    {
        case StatusCodes.Status403Forbidden:
            await context.Response.WriteAsync(new SimpleErrorMessage("Access Denied", "AccessDenied").ToJson());
            break;
        case StatusCodes.Status400BadRequest:
            await context.Response.WriteAsync(new SimpleErrorMessage("Bad request", "BadRequest").ToJson());
            break;
        case StatusCodes.Status404NotFound:
            await context.Response.WriteAsync(new SimpleErrorMessage("The specified footprint does not exist", "NoSuchFootprint").ToJson());
            break;
        case StatusCodes.Status501NotImplemented:
            await context.Response.WriteAsync(new SimpleErrorMessage("The specified Action or header you provided implies functionality that is not implemented", "NotImplemented").ToJson());
            break;
        case StatusCodes.Status401Unauthorized:
            await context.Response.WriteAsync(new SimpleErrorMessage("The specified access token is invalid or has expired", "TokenInvalid").ToJson());
            break;
        case StatusCodes.Status500InternalServerError:
            await context.Response.WriteAsync(new SimpleErrorMessage("The specified Action or header you provided implies functionality that is not implemented", "InternalError").ToJson());
            break;
    }
});

app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
