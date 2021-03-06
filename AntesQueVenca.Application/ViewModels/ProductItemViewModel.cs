using System;
using System.Collections.Generic;
using System.Text;

namespace AntesQueVenca.Application.ViewModels
{
    public class ProductItemViewModel
    {
        public int ProductItemId { get; set; }

        public int ProductId { get; set; }

        public virtual ProductViewModel Product { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Price { get; set; }

        public decimal PriceFrom { get; set; }

        public int RetailerId { get; set; }

        public bool Sold { get; set; }

        public int Quantity { get; set; }

        public decimal AmountTotalItem 
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}
