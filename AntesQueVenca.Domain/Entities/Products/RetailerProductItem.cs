namespace AntesQueVenca.Domain.Entities
{
    public class RetailerProductItem
    {
        public int RetailerProductItemId { get; set; }

        public int RetailerId { get; set; }

        public virtual Retailer Retailer { get; set; }

        public int ProductItemId { get; set; }

        public virtual ProductItem ProductItem { get; set; }

        public int Quantity { get; set; }
    }
}
