using AntesQueVenca.Domain.Entities;

namespace AntesQueVenca.Application.ViewModels
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }

        public int PersonTypeId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public string Phone { get; set; }

        public Address Address { get; set; }
    }
}
