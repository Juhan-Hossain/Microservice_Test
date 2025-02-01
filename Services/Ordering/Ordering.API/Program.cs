using Microsoft.Extensions.DependencyInjection;
using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;
using MediatR;
using BuildingBlocks.Behaviors;
var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
//Add services to the container
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

 
var app = builder.Build();

//configuring http request lifecycle
app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitialDatabaseAsync();
}

app.Run();
