using System.Reflection;
using Serilog;
using QueueServices;
using Serilog.Sinks.Elasticsearch;
using Webshop.Search.Application.Extensions;
using Webshop.Search.Domain.AggregateRoots;
using Webshop.Search.Persistence;
using Webshop.Search.Persistence.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = builder.Configuration;
configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
configuration.AddJsonFile(
    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
    optional: true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddRabbitMQServices();
builder.Services.AddElasticSearch(configuration);

builder.Services.AddSingleton<IWriteRepository<ProductEntity>, SearchWriteRepository<ProductEntity>>();
builder.Services.AddSingleton<SearchReadRepository<ProductEntity>>();

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration);
});

var app = builder.Build();

Log.Logger.Debug("Current environment is: {environment}", environment);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();

static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    var indexFormat =
        $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = indexFormat
    };
}
