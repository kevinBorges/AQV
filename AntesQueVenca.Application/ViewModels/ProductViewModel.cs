using System;
using System.Collections.Generic;
using System.Text;

namespace AntesQueVenca.Application.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Price { get; set; }

    }
}
