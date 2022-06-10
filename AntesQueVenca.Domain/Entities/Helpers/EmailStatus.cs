using System;
using System.Collections.Generic;
using System.Text;

namespace AntesQueVenca.Domain.Entities.Helpers
{
    public class EmailStatus
    {
        public int EmailStatusId { get; set; }
        public string Name { get; set; }
        public List<Email> Emails { get; set; }
    }
}
