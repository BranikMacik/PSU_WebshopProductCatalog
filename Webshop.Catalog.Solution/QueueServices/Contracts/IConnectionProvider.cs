﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueServices.Contracts
{
    public interface IConnectionProvider
    {
        IConnection GetConnection();
    }
}
