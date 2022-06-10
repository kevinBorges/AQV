using System.ComponentModel.DataAnnotations;

namespace AntesQueVenca.Ecommerce.ViewModels
{
    public class ProductViewModel
    {
        [ScaffoldColumn(false)]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public decimal PriceFrom { get; set; }

        public string Description { get; set; }

        public string ExpirationDate { get; set; }

        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public decimal Discount 
        {
            get 
            {
                return PriceFrom - Price;
            }
        }
    }
}
