using AntesQueVenca.Domain.Entities.Consumers;
using AntesQueVenca.Domain.Enuns;
using System;
using System.Collections.Generic;

namespace AntesQueVenca.Domain.Entities
{
    public class Order : EntityBase
    {
        public int OrderId { get; set; }

        public string Comments { get; set; }

        public int ConsumerId { get; set; }

        public virtual Consumer Consumer { get; set; }

        public OrderStatusEnum Status { get; set; }

        public DateTime WithdrawDate { get; set; }

        public DeliveryTypeEnum DeliveryType { get; set; }

        public PaymentTypeEnum PaymentType { get; set; }

        public decimal TotalPrice { get; set; }

        public bool NeedThing { get; set; }

        public decimal Thing { get; set; }

        public decimal DeliveryPrice { get; set; }

        public int RetailerId { get; set; }

        public virtual Retailer Retailer { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
