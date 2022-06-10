using System;
using System.Collections.Generic;
using System.Text;

namespace AntesQueVenca.Domain.Entities
{
    public class PostalCodeDetail
    {
        public int PostalCodeDetailId { get; set; }

        public decimal Price { get; set; }

        public string PostalCode { get; set; }
    }
}
