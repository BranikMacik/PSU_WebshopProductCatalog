using Newtonsoft.Json;
using QueueServices.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueServices.Features.ProcessingServices
{
    public class OrderConsumer<OrderDataTransferObject> : IConsumer<OrderDataTransferObject>, IDisposable
    {
        private readonly IModel _channel;

        public OrderConsumer(IModelProvider modelProvider)
        {
            _channel = modelProvider.GetModel();

            _channel.QueueDeclare(queue: ConnectionConstants.OrderQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            Console.WriteLine(" [*] Waiting for messages.");
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                // Deserialize the JSON to your orderDto
                var message = JsonConvert.DeserializeObject<OrderDataTransferObject>(Encoding.UTF8.GetString(body));
                Console.WriteLine($" [x] Received '{message}'");
                // Retrieve the operation type from message properties
                var operationType = ea.BasicProperties.Headers?["OperationType"] as string;

                // Determine the operation based on the retrieved type
                switch (operationType)
                {
                    case "Create":
                        CreateOrder(message);
                        break;
                    case "Update":
                        UpdateOrder(message);
                        break;
                    case "Read":
                        ReadOrder(message);
                        break;
                    case "Delete":
                        DeleteOrder(message);
                        break;
                    default:
                        Console.WriteLine($"Unknown operation type: {operationType}");
                        break;
                }
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

             _channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);

        }
        private void CreateOrder(OrderDataTransferObject orderDto)
        {
            // Implementation to create and save the order in the database
            Console.WriteLine($"Creating order: {orderDto}");
        }

        private void UpdateOrder(OrderDataTransferObject orderDto)
        {
            // Implementation to update the order in the database
            Console.WriteLine($"Updating order: {orderDto}");
        }

        private void ReadOrder(OrderDataTransferObject orderDto)
        {
            // Implementation to read the order from the database
            Console.WriteLine($"Reading order: {orderDto}");
        }

        private void DeleteOrder(OrderDataTransferObject orderDto)
        {
            // Implementation to delete the order from the database
            Console.WriteLine($"Deleting order: {orderDto}");
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
        }
    }
}
