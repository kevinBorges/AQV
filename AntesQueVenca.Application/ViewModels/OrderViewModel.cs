using AntesQueVenca.Domain.Entities.Consumers;
using AntesQueVenca.Domain.Enuns;
using System;
using System.Collections.Generic;

namespace AntesQueVenca.Application.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        public int OrderId { get; set; }

        public string Comments { get; set; }

        public int ConsumerId { get; set; }

        public virtual ConsumerViewModel Consumer { get; set; }

        public OrderStatusEnum Status { get; set; }

        public string StatusClass
        {
            get
            {
                if (Status == OrderStatusEnum.Retirado)
                    return "btn btn-success";
                else if (Status == OrderStatusEnum.Entregue)
                    return "btn btn-success";
                else if (Status == OrderStatusEnum.Cancelado)
                    return "btn btn-danger";
                else if (Status == OrderStatusEnum.Expirado)
                    return "btn btn-info";
                else
                    return "btn btn-warning";
            }
        }

        public string CardClass 
        {
            get 
            {
                if (Status == OrderStatusEnum.Retirado)
                    return "card card-border-c-green";
                else if (Status == OrderStatusEnum.Entregue)
                    return "card card-border-c-green";
                else if (Status == OrderStatusEnum.Cancelado)
                    return "card card-border-c-red";
                else if (Status == OrderStatusEnum.Expirado)
                    return "card card-border-c-blue";
                else
                    return "card card-border-c-yellow";
            }
        }

        public string WithdrawDate { get; set; }

        public List<ProductItemViewModel> Products { get; set; }

        public decimal TotalPrice { get; set; }

        public DeliveryTypeEnum DeliveryType { get; set; }

        public string Delivery { get; set; }

        public string Thing { get; set; }

        public PaymentTypeEnum PaymentType { get; set; }

        public string CriadoEm { get; set; }
    }
}
