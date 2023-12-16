using MediatR;
using QueueServices;
using QueueServices.Contracts;
using QueueServices.Features.Dtos;
using System.Reflection;
using Webshop.Application;
using Webshop.Application.Contracts;
using Webshop.Data.Persistence;
using Webshop.Order.Application;
using Webshop.Order.Application.Contracts.Persistence;
using Webshop.Order.Persistence;
using Webshop.Search.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<`IOrderRepository, OrderRepository>();

//add own services
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<DataContext, DataContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IDispatcher>(sp => new Dispatcher(sp.GetService <IMediator>()));
builder.Services.AddOrderApplicationServices();

builder.Services.AddRabbitMQServices();


var app = builder.Build();

var orderConsumer = app.Services.GetRequiredService<IConsumer<OrderDataTransferObject>>();
orderConsumer.StartConsuming();

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
