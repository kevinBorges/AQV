namespace AntesQueVenca.Application.ViewModels
{
    public class ConsumerViewModel
    {
        public int ConsumerId { get; set; }

        public int PersonId { get; set; }

        public virtual PersonViewModel Person { get; set; }
    }
}
