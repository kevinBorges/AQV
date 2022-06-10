using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AntesQueVenca.Domain.Enuns
{
    public enum DeliveryTypeEnum
    {
        [Description("Retirada")]
        Withdrawal=1,
        [Description("Entrega")]
        Delivery =2
    }
}
