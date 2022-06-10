using System.ComponentModel;

namespace AntesQueVenca.Domain.Enuns
{
    public enum AddressTypeEnum
    {
        [Description("Casa")]
        Casa=1,
        [Description("Apartamento")]
        Apartamento =2,
        [Description("Comercial")]
        Comercial = 3,
        [Description("Outro")]
        Outro=4
    }
}
