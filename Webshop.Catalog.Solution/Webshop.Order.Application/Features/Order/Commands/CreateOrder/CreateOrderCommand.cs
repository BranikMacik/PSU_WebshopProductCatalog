using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Application.Contracts;
using Webshop.Domain.AggregateRoots;

namespace Webshop.Order.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommand : ICommand
    {
        public CreateOrderCommand(int customerId, DateTime dateOfIssue, DateTime dueDate, int discount, Dictionary<int, int> orderedProductIdsAndAmounts)
        {
            Ensure.That(customerId, nameof(customerId)).IsGt(0);
            CustomerId = customerId;
            Ensure.That(dateOfIssue, nameof(dateOfIssue)).IsNot(DateTime.MinValue);
            DateOfIssue = dateOfIssue;
            Ensure.That(dueDate, nameof(dueDate)).IsNot(DateTime.MinValue);
            DueDate = dueDate;
            Ensure.That(orderedProductIdsAndAmounts, nameof(orderedProductIdsAndAmounts)).IsNotNull();
            Ensure.That(orderedProductIdsAndAmounts, nameof(orderedProductIdsAndAmounts)).HasItems();
            OrderedProductIdsAndAmounts = orderedProductIdsAndAmounts;
            Ensure.That(discount, nameof(discount)).IsInRange(0,15);
            Discount = discount;
        }

        public int CustomerId { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DueDate { get; set; }
        public int Discount { get; set; }
        public Dictionary<int, int> OrderedProductIdsAndAmounts { get; set; }
    }
}
