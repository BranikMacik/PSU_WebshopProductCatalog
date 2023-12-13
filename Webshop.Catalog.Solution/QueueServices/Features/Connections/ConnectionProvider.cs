using QueueServices.Contracts;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueServices.Features.Connections
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly IConnection _connection;

        /*  //preferable constructor
         * public ConnectionProvider(string connectionString)
         *  {
         *      var factory = new ConnectionFactory
         *      {
         *          Uri = new Uri(connectionString),
         *      };
         *
         *      _connection = factory.CreateConnection();
         *  }
         */
        public ConnectionProvider(IConnection connection)
        {
            _connection = connection;
        }


        public IConnection GetConnection()
        {
            return _connection;
        }
    }
}
