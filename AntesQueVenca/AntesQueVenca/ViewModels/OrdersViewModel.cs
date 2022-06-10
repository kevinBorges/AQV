using AntesQueVenca.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AntesQueVenca.ViewModels
{
    public class OrdersViewModel
    {
        public ObservableCollection<Order> MyOrders { get; private set; }

        public OrdersViewModel()
        {
            MyOrders = new ObservableCollection<Order>(new List<Order> { 
                new Order 
                {
                    WithdrawDate=DateTime.Now
                },
                new Order
                {
                    WithdrawDate=DateTime.Now.AddDays(1)
                }
            });
        }
    }
}
