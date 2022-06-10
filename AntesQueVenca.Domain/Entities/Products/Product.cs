using System;
using System.Collections.Generic;

namespace AntesQueVenca.Domain.Entities
{
    public class Product : EntityBase
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ProductItem> ProductItems { get; set; }
    }
}
