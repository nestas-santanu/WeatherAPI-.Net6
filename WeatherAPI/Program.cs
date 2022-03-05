using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using WeatherAPI.APIs.VS.AutofacModule;
using WeatherAPI.APIs.WMO.AutofacModule;
using WeatherAPI.APIs.WMO.Configuration;
using WeatherAPI.APIs.WMO.Response.Models;
using WeatherAPI.Filters.Swagger;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

//logging
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

// Add services to the container.
//configuration service
builder.Services.Configure<Forecast>(
    builder.Configuration.GetSection(Forecast.Path));
builder.Services.Configure<Cities>(
    builder.Configuration.GetSection(Cities.Path));
builder.Services.Configure<Organization>(
    builder.Configuration.GetSection(Organization.Path));

//DI -Autofac
//https://stackoverflow.com/questions/63407601/how-is-autofac-better-than-microsoft-extensions-dependencyinjection
// Call UseServiceProviderFactory on the Host sub property
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Call ConfigureContainer on the Host sub property
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    // Declare services with proper lifetime
    // declaring services manually

    //weather data provider: visual studio
    builder.RegisterModule(new VSModule());

    //weather data provider: World Meteorological Organization (WMO)
    builder.RegisterModule(new WMOModule());

    //http service
    builder
        .RegisterType<Weather.HTTPService.Request>()
        .As<Weather.HTTPService.Service.IRequest>()
        .InstancePerLifetimeScope();
});

//http service: adds an instance of the HttpClient
builder.Services.AddHttpClient();

//Caching services.
//https://www.nuget.org/packages/LazyCache/
//We read cities from a file. Cities don't change, so the contents of the file are cached.
//see class Weather.DataProvider.WMO.Cities.Data.FromFile.Read
builder.Services.AddLazyCache();

//Exceptions using ProblemsDetails
builder.Services.AddProblemDetails(options =>
{
    options.IncludeExceptionDetails
    = (_, _) => builder.Environment.IsDevelopment()
        || builder.Environment.IsStaging();

    options.Map<WMOException>(exception => new WMOExceptionProblemDetails
    {
        Type = exception.Type,
        Title = exception.Title,
        Status = exception.Status,
        Detail = exception.Detail,
        Instance = exception.Instance,
        Endpoints = exception.Endpoints
    });
});

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressMapClientErrors = true;
});

//this is required to inject the LinkGenerator for generating embedded links in the response.
//see implementation of IEndpoints.
builder.Services.AddRouting();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    //Changing the SwaggerDoc parameter 'name' to value other than the initial one
    //raises a 404 error. Why?
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Weather API",
        Description = "An .Net 6 Web API for 'real time' weather forecast. <br/> " +
        "Also includes the Visual Studio weather forecaster.",
        //Contact = new OpenApiContact
        //{
        //    Name = "Example Contact",
        //    Url = new Uri("https://example.com/contact")
        //},
    });
    options.DocumentFilter<RemoveSchemasFilter>();

    const string? xmlFilename = "WeatherAPI.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

WebApplication? app = builder.Build();

//setup the app's root folders
//this is useful when reading files in the system
AppDomain.CurrentDomain.SetData("ContentRootPath", app.Environment.ContentRootPath);

//// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseProblemDetails();

app.MapControllers();

app.Run();