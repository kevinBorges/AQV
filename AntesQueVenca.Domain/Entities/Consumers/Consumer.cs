namespace AntesQueVenca.Domain.Entities.Consumers
{
    public class Consumer : EntityBase
    {
        public int ConsumerId { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
