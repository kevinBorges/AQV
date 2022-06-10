using AntesQueVenca.Domain.Entities;
using AntesQueVenca.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AntesQueVenca.Mobile.Admin.ViewModels
{
    public class OrdersViewModel : ViewModelBase<ObservableCollection<Order>>
    {
        public OrdersViewModel()
        {
            Entity = new ObservableCollection<Order>(new List<Order>
            {
                new Order
                {
                    OrderId = 14115,
                    Comments = "Teste" ,
                    Consumer = new Domain.Entities.Consumers.Consumer { Person = new PhysicalPerson { Name = "Felipe" } },
                    Status = Domain.Enuns.OrderStatusEnum.Pendente,
                    WithdrawDate = DateTime.Now
                },
                new Order
                {
                    OrderId = 1416,
                    Comments = "Teste Teste",
                    Consumer = new Domain.Entities.Consumers.Consumer { Person = new PhysicalPerson { Name = "Leandro" } },
                    Status = Domain.Enuns.OrderStatusEnum.Retirado,
                    WithdrawDate = DateTime.Now.AddDays(-1)
                }
            });
        }
    }
}
