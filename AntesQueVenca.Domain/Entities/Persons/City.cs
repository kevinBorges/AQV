namespace AntesQueVenca.Domain.Entities
{
    public class City
    {
        public int CityId { get; set; }

        public int IbgeId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public int? StateId { get; set; }

        public virtual State State { get; set; }
    }
}
