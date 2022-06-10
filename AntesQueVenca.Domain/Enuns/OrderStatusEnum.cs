using System.ComponentModel;

namespace AntesQueVenca.Domain.Enuns
{
    public enum OrderStatusEnum
    {
        [Description("Pendente")]
        Pendente=1,
        [Description("Retirado")]
        Retirado=2,
        [Description("Cancelado")]
        Cancelado =3,
        [Description("Expirado")]
        Expirado = 4,
        [Description("Entregue")]
        Entregue = 5,
    }
}
