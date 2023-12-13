using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueServices.Features.Dtos
{
    public class OrderDataTransferObject
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DueDate { get; set; }
        public int Discount { get; set; }
        public Dictionary<int, int> OrderedProductIdsAndAmounts { get; set; }
    }
}
