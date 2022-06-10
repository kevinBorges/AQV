namespace AntesQueVenca.Domain.Entities.Helpers
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Retorno { get; set; }
        public string Url { get; set; }
    }
}
