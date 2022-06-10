using AntesQueVenca.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AntesQueVenca.ViewModels
{
    public class HomeViewModel
    {
        public ObservableCollection<Category> Categories { get; private set; }

        public HomeViewModel()
        {
            Categories = new ObservableCollection<Category>(new List<Category> {
                new Category 
                {
                    Name = "HortiFruti"
                },
                new Category
                {
                    Name = "HortiFruti"
                },
                new Category
                {
                    Name = "HortiFruti"
                },
                new Category
                {
                    Name = "HortiFruti"
                },
                new Category
                {
                    Name = "HortiFruti"
                },
                new Category
                {
                    Name = "HortiFruti"
                },
                new Category
                {
                    Name = "HortiFruti"
                },
                new Category
                {
                    Name = "HortiFruti"
                },
            });
        }
    }
}
