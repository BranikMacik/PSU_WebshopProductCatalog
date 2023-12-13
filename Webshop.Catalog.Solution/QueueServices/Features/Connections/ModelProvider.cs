using QueueServices.Contracts;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueServices.Features.Connections
{
    public class ModelProvider : IModelProvider
    {
        private readonly IConnection _connection;
        private readonly IModel _model;

        public ModelProvider(IConnection connection)
        {
            _connection = connection;
            _model = _connection.CreateModel();
        }

        public IModel GetModel()
        {
            return _model;
        }
    }
}
