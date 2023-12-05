using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Domain.Common;

namespace Webshop.Order.Domain.AggregateRoots
{
    public class Order : AggregateRoot
    {
        public Order(int customerId, DateTime dateOfIssue, DateTime dueDate, int discount, Dictionary<int, int> orderedProductIdsAndAmounts)
        {
            Ensure.That(dateOfIssue != DateTime.MinValue);
            DateOfIssue = dateOfIssue;

            Ensure.That(dueDate != DateTime.MinValue);
            DueDate = dueDate;

            Ensure.That(customerId > 0);
            CustomerId = customerId;

            Ensure.That(discount < 0 && discount > 15);
            Discount = discount;

            Ensure.That(orderedProductIdsAndAmounts).IsNotNull();
            Ensure.That(orderedProductIdsAndAmounts.Count > 0);
            OrderedProductIdsAndAmounts = orderedProductIdsAndAmounts;
        }

        public Order() { } //for ORM

        public int CustomerId { get; set; }
        public DateTime DateOfIssue { get; set; }
        /// <summary>
        /// The date, order is expected to be delivered
        /// </summary>
        public DateTime DueDate { get; set; }
        public int Discount { get; set; }
        public Dictionary<int, int> OrderedProductIdsAndAmounts { get; set; }
    }


}
