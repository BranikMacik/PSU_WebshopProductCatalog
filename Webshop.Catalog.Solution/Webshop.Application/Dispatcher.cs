﻿using EnsureThat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Application.Contracts;
using Webshop.Domain.Common;

namespace Webshop.Application
{
    public class Dispatcher : IDispatcher
    {
        public Dispatcher(IMediator mediator)
        {
            Ensure.That(mediator).IsNotNull();
            Mediator = mediator;
        }

        public IMediator Mediator { get; }

        public Task<Result<T>> Dispatch<T>(IQueryHandler<T> query)
        {
            Ensure.That(query, nameof(query)).IsNotNull();
            return Mediator.Send(query);
        }

        public Task<Result> Dispatch(ICommand command)
        {
            Ensure.That(command, nameof(command)).IsNotNull();
            return Mediator.Send(command);
        }
    }
}
