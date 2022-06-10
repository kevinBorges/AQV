using System.Collections.Generic;

namespace AntesQueVenca.Domain.Entities
{
    public abstract class Person : EntityBase
    {
        public int PersonId { get; set; }

        public int PersonTypeId { get; set; }

        public virtual PersonType PersonType { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public virtual IList<Phone> Phones { get; set; }

        public virtual IList<Address> Addresses { get; set; }
        public virtual ICollection<Retailer> Retailers { get; set; }
    }
}
