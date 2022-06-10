using AntesQueVenca.Domain.Entities;

namespace AntesQueVenca.Ecommerce.ViewModels
{
    public class ConsumerViewModel
    {
        public string CPF { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public Address Address { get; set; }
    }
}
