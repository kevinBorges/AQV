using System.Collections.Generic;

namespace AntesQueVenca.Domain.Entities
{
    public class Retailer : EntityBase
    {
        public int RetailerId { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
        //public virtual ICollection<ProductItem> ProductItems { get; set; }
        public virtual ICollection<RetailerProductItem> RetailerProducts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
