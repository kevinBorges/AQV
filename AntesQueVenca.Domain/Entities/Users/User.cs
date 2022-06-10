using System.Collections.Generic;

namespace AntesQueVenca.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int? PersonId { get; set; }

        public virtual Person Person { get; set; }
        
        public virtual ICollection<Person> PersonsThatCreated { get; set; }
        public virtual ICollection<Person> PersonsThatModified { get; set; }
        public virtual ICollection<Product> ProductsThatCreated { get; set; }
        public virtual ICollection<Product> ProductsThatModified { get; set; }
        public virtual ICollection<Category> CategoryThatCreated { get; set; }
        public virtual ICollection<Category> CategoryThatModified { get; set; }
        public virtual ICollection<Order> OrdersThatModified { get; set; }
        public virtual ICollection<Order> OrdersThatCreated { get; set; }
        public virtual ICollection<Retailer> RetailersThatCreated { get; set; }
        public virtual ICollection<Retailer> RetailersThatModified { get; set; }
        public virtual ICollection<ProductItem> ProductItemsThatCreated { get; set; }
        public virtual ICollection<ProductItem> ProductItemsThatModified { get; set; }
    }
}
