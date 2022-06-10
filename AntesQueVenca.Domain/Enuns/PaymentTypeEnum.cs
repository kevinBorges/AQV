using System.ComponentModel;

namespace AntesQueVenca.Domain.Enuns
{
    public enum PaymentTypeEnum
    {
        [Description("Dinheiro")]
        Dinheiro=1,
        [Description("Pix")]
        Pix =2,
        [Description("Cartão de Crédito")]
        CartaoCredito = 3,
        [Description("Cartão de Débito")]
        CartaoDebito = 4
    }
}
