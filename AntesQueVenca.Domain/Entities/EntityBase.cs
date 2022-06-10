using AntesQueVenca.Domain.Enuns;
using Newtonsoft.Json;
using System;

namespace AntesQueVenca.Domain.Entities
{
    public class EntityBase
    {
        public bool Deleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedById { get; set; }

        [JsonIgnore]
        public virtual User CreatedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public int LastModifiedById { get; set; }

        [JsonIgnore]
        public virtual User LastModifiedBy { get; set; }

        
        public void SetFieldsInsert(int userId)
        {
            Deleted = false;
            CreatedDate = DateTime.Now;
            CreatedById = userId;
            LastModifiedDate = DateTime.Now;
            LastModifiedById = userId;
        }

        public void SetFieldsUpdate(int userId)
        {
            LastModifiedDate = DateTime.Now;
            LastModifiedById = userId;
        }
    }
}
