using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Application.Contracts;

namespace Webshop.Catalog.Application.Features.Product.Commands.UpdateAmountInStockForProduct
{
    public class UpdateAmountInStockForProductCommand : ICommand
    {
        public UpdateAmountInStockForProductCommand(int id, int amountInStock)
        {
            Id = id;
            AmountInStock = amountInStock;
        }

        public int Id { get; private set; }
        public int AmountInStock { get; private set; }
    }
}
