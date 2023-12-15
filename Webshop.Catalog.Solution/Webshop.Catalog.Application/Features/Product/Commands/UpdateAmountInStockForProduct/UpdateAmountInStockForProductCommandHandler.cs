using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Webshop.Application.Contracts;
using Webshop.Catalog.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.Catalog.Application.Features.Product.Commands.UpdateAmountInStockForProduct
{
    public class UpdateAmountInStockForProductCommandHandler : ICommandHandler<UpdateAmountInStockForProductCommand>
    {
        private ILogger<UpdateAmountInStockForProductCommand> logger;
        private IProductRepository productRepository;
        public UpdateAmountInStockForProductCommandHandler(ILogger<UpdateAmountInStockForProductCommand> logger, IProductRepository productReopsitory)
        {
            this.logger = logger;
            this.productRepository = productReopsitory;
        }

        public async Task<Result> Handle(UpdateAmountInStockForProductCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                Domain.AggregateRoots.Product product = new Domain.AggregateRoots.Product();
                product.AmountInStock = command.AmountInStock;
                product.Id = command.Id;
                await this.productRepository.UpdateAmountInStockAsync(product);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogCritical(ex, ex.Message);
                return Result.Fail(Errors.General.UnspecifiedError(ex.Message));
            }
        }
    }
}
