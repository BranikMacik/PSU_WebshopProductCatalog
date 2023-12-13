using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingService
{
    internal class OrderPublisher
    {
        private IConnection connection;
        private IModel channel;
        private string replyQueueName;
       
        public void Connect() 
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = ConnectionConstants.HostName

            };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.QueueDeclare(queue: ConnectionConstants.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Disconnect() 
        {
            channel = null;

            if (connection.IsOpen) 
            {
                connection.Close();
            }

            connection.Dispose();
            connection = null;
        }

        /*
        public void SendCreateOrder(OrderDto orderDto)
        { 
            
        }
        public void SendOrderMessage()
        {
            var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(orderDto));
        }
        
            string message = "Hello, RabbitMQ!";
        var body = Encoding.UTF8.GetBytes(message);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

            channel.BasicPublish(exchange: "", routingKey: "order_queue", basicProperties: properties, body: body);

            Console.WriteLine($" [x] Sent '{message}'");
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
        */
    }
}
