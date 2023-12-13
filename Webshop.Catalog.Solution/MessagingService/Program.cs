using MessagingService;
using RabbitMQ.Client;
using System;
using System.Text;

class Program
{
    //Publisher service
    static void Main()
    {
        OrderPublisher publisher = new OrderPublisher();
        
        {
            /*
            string message = "Hello, RabbitMQ!";
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "", routingKey: "order_queue", basicProperties: properties, body: body);

            Console.WriteLine($" [x] Sent '{message}'");
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();*/
        }
    }


}