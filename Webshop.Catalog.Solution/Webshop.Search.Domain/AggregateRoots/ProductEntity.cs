using Webshop.Domain.Common;

namespace Webshop.Search.Domain.AggregateRoots
{
    public class ProductEntity : ElasticEntityBase
    {

        private const string IndexName = "search_product";
        public ProductEntity() : base(IndexName, "product")
        {

        }

        public string Name { get; private set; }
        public string Description { get; set; }
        /// <summary>
        /// Stock keeping unit
        /// </summary>
        public string SKU { get; set; }
        public int AmountInStock { get; set; }

        /// <summary>
        /// The price is represented in the lowest monetary value. For Euros this is cents, this means that to show the price on the web site, we should divide with 100
        /// </summary>
        public int Price { get; set; }
        public string Currency { get; set; }
    }
}
