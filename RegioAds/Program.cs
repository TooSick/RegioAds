using Microsoft.OpenApi.Models;
using RegioAds.Api.Middlewares;
using RegioAds.Application;
using RegioAds.Infrastructure;
using RegioAds.Infrastructure.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AdPlatform",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.Configure<FileCofig>(builder.Configuration.GetSection("FileConfig"));

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    c.RoutePrefix = string.Empty; // Swagger открывается сразу на "/"
});

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();