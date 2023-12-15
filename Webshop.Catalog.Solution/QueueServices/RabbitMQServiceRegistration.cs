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
using Webshop.Order.Domain.AggregateRoots;

namespace QueueServices
{
    public static class RabbitMQServiceRegistration
    {
        public static IServiceCollection AddRabbitMQServices(this IServiceCollection services)
        {
            services.AddSingleton<IConnection>(sp =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = ConnectionConstants.HostName,
                };

                return factory.CreateConnection();
            });

            services.AddSingleton<IConnectionProvider, ConnectionProvider>();
            services.AddSingleton<IModelProvider, ModelProvider>();
            services.AddSingleton<IConsumer<Order>, OrderConsumer>();

            return services;
        }
    }
}

