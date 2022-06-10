using System;
using System.Collections.Generic;
using System.Text;

namespace AntesQueVenca.Domain.Entities
{
    public class ProductItem: EntityBase
    {
        public int ProductItemId { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Price { get; set; }

        public decimal PriceFrom { get; set; }

        //public int RetailerId { get; set; }
        //public virtual Retailer Retailer { get; set; }

        public bool Sold { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<RetailerProductItem> RetailerProducts { get; set; }
    }
}