using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueueServices;
using QueueServices.Contracts;
using QueueServices.Features.Connections;
using QueueServices.Features.Dtos;
using QueueServices.Features.ProcessingServices;
using RabbitMQ.Client;
using System;
using System.Reflection;
using Webshop.Application;
using Webshop.Application.Contracts;
using Webshop.Data.Persistence;
using Webshop.Order.Application;
using Webshop.Order.Application.Contracts.Persistence;
using Webshop.Order.Application.Features.Order.Dtos;
using Webshop.Order.Persistence;

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
