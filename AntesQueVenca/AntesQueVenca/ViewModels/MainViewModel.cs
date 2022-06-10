using AntesQueVenca.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntesQueVenca.ViewModels
{
    public class MainViewModel: ViewModelBase<object>
    {
        private ProductsViewModel _productsViewModel;
        private OrdersViewModel _ordersViewModel;
        private HomeViewModel _homeViewModel;
        private ProfileViewModel _profileViewModel;

        public IList<AppPage> Pages { get; private set; }

        public MainViewModel()
        {
            _productsViewModel = new ProductsViewModel();
            _ordersViewModel = new OrdersViewModel();
            _homeViewModel = new HomeViewModel();
            _profileViewModel = new ProfileViewModel();

            Pages = GetPages();
        }

        private IList<AppPage> GetPages()
        {
            return new List<AppPage>
            {
                new AppPage
                {
                    Name = "Início",
                    Icon = "homeIcon",
                    Type = AppPageType.Home,
                    ViewModel = _homeViewModel
                },
                new AppPage
                {
                    Name = "Produtos",
                    Icon = "productsIcon",
                    Type = AppPageType.Products,
                    ViewModel = _productsViewModel
                },
                new AppPage
                {
                    Name = "Pedidos",
                    Icon = "ordersIcon",
                    Type = AppPageType.Orders,
                    ViewModel = _ordersViewModel
                },
                new AppPage
                {
                    Name = "Perfil",
                    Icon = "profileIcon",
                    Type = AppPageType.Profile,
                    ViewModel = _profileViewModel
                }
            };
        }
    }
}
