using System;
using System.Collections.Generic;
using System.Text;

namespace AntesQueVenca.Domain.Entities
{
    public class OrderProduct
    {
        public int OrderProductId { get; set; }

        public int ProductItemId { get; set; }

        public virtual ProductItem ProductItem { get; set; }

        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
