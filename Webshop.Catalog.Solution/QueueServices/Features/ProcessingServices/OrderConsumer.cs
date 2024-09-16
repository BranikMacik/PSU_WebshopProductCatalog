using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QueueServices.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Webshop.Application.Contracts;
using Webshop.Catalog.Application.Features.Product.Commands.UpdateAmountInStockForProduct;
using Webshop.Domain.Common;
using Webshop.Order.Domain.AggregateRoots;

namespace QueueServices.Features.ProcessingServices
{
    public class OrderConsumer : IConsumer<Order>, IDisposable
    {
        private readonly IModel _channel;
        private readonly ILogger<OrderConsumer> logger;
        private readonly IDispatcher dispatcher;

        public OrderConsumer(IDispatcher dispatcher, ILogger<OrderConsumer> logger, IModelProvider modelProvider)
        {
            this.dispatcher = dispatcher;
            this.logger = logger;

            _channel = modelProvider.GetModel();

            _channel.QueueDeclare(queue: ConnectionConstants.OrderQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            Console.WriteLine(" [*] Waiting for messages.");
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                // Deserialize the JSON to your orderDto
                var message = JsonConvert.DeserializeObject<Order>(Encoding.UTF8.GetString(body));
                logger.LogInformation($"Received order: {message}");
                // Retrieve the operation type from message properties
                var operationType = ea.BasicProperties.Headers?["OperationType"] as string;

                // Determine the operation based on the retrieved type
                switch (operationType)
                {
                    case "UpdateItemCount":
                        await UpdateItemCount(message);
                        break;
                    /*case "Update":
                        UpdateOrder(message);
                        break;
                    case "Read":
                        ReadOrder(message);
                        break;
                    case "Delete":
                        DeleteOrder(message);
                        break;*/
                    default:
                        logger.LogInformation($"Unknown operation type: {operationType}");
                        break;
                }
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

             _channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);

        }

        private async Task UpdateItemCount(Order order)
        {
            try
            {
                var orderedProducts = order.OrderedProductIdsAndAmounts;
                foreach (KeyValuePair<int, int> product in orderedProducts)
                {
                    int productId = product.Key;
                    int amount = product.Value;
                    UpdateAmountInStockForProductCommand command = new UpdateAmountInStockForProductCommand(productId, amount);
                    await this.dispatcher.Dispatch(command);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Specific error: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
        }
    }
}
