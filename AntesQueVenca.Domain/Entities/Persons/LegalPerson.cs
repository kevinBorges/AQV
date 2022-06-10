namespace AntesQueVenca.Domain.Entities
{
    public class LegalPerson : Person
    {
        public string CNPJ { get; set; }

        public string FantasyName { get; set; }

        public string CompanyName { get; set; }
    }
}
