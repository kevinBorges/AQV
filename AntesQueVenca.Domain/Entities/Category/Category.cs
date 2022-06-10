using System.Collections.Generic;

namespace AntesQueVenca.Domain.Entities
{
    public class Category : EntityBase
    {
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
