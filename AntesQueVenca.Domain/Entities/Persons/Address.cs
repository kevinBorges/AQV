using AntesQueVenca.Domain.Enuns;

namespace AntesQueVenca.Domain.Entities
{
    public class Address
    {
        public int AddressId { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Neighborhood { get; set; }

        public string Country { get; set; }
     
        public string Complement { get; set; }

        public string CityName { get; set; }

        public string UF { get; set; }


        public int? CityId { get; set; }

        public virtual City City { get; set; }

        public AddressTypeEnum AddressType { get; set; }
    }
}
