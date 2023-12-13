using Newtonsoft.Json;
using QueueServices.Contracts;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Order.Application.Features.Order.Requests;

namespace QueueServices.Features.MessagingServices
{
    public class OrderPublisher<TMessage> : IPublisher<TMessage>
    {
        private readonly IModel _channel;

        public OrderPublisher(IModelProvider modelProvider)
        {
            _channel = modelProvider.GetModel();
            _channel.QueueDeclare(queue: ConnectionConstants.OrderQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            
        }

        public void PublishCreateOrder(CreateOrderRequest request, string operationType)
        {
            try
            {
                // Send the order to RabbitMQ with the operation type
                var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
                var properties = _channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>
                {
                    ["OperationType"] = operationType,
                };

                _channel.BasicPublish(exchange: "", routingKey: ConnectionConstants.OrderQueueName, basicProperties: properties, body: messageBody);
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing message: {ex.Message}");
            }
        }
    }
}
