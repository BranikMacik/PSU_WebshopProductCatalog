using Microsoft.Extensions.DependencyInjection;
using QueueServices.Contracts;
using QueueServices.Features.Connections;
using QueueServices.Features.Dtos;
using QueueServices.Features.ProcessingServices;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueServices
{
    public static class RabbitMQServiceRegistration
    {
        public static IServiceCollection AddRabbitMQServices(this IServiceCollection services)
        {
            services.AddScoped<IConnection>(sp =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = ConnectionConstants.HostName,
                };

                return factory.CreateConnection();
            });

            services.AddScoped<IConnectionProvider, ConnectionProvider>();
            services.AddScoped<IModelProvider, ModelProvider>();
            services.AddScoped<IConsumer<OrderDataTransferObject>, OrderConsumer<OrderDataTransferObject>>();

            return services;
        }
    }
}

