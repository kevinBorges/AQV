using AntesQueVenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AntesQueVenca.ViewModels
{
    public class ProductsViewModel
    {
        public ObservableCollection<Product> Products { get; private set; }

        public ProductsViewModel()
        {
            var products = new List<Product>();
            products.Add(new Product{ ProductId=1, Name="Tomate", Image= "tomateIcon" });
            products.Add(new Product { ProductId = 1, Name = "Tomate", Image = "tomateIcon" });
            products.Add(new Product { ProductId = 2, Name = "Tomate", Image = "tomateIcon" });
            products.Add(new Product { ProductId = 3, Name = "Tomate", Image = "tomateIcon" });
            products.Add(new Product { ProductId = 4, Name = "Tomate", Image = "tomateIcon" });
            products.Add(new Product { ProductId = 5, Name = "Tomate", Image = "tomateIcon" });
            products.Add(new Product { ProductId = 6, Name = "Tomate", Image = "tomateIcon" });
            products.Add(new Product { ProductId = 7, Name = "Tomate", Image = "tomateIcon" });
            products.Add(new Product { ProductId = 8, Name = "Tomate", Image = "tomateIcon" });
            products.Add(new Product { ProductId = 9, Name = "Tomate", Image = "tomateIcon" });
            products.Add(new Product { ProductId = 10, Name = "Tomate", Image = "tomateIcon" });
            products.Add(new Product { ProductId = 11, Name = "Tomate", Image = "tomateIcon" });
            Products = new ObservableCollection<Product> (products);
        }
    }
}
