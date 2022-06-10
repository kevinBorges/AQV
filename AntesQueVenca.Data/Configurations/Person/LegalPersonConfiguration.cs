using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntesQueVenca.Data.Configurations
{
    public class LegalPersonConfiguration : IEntityTypeConfiguration<LegalPerson>
    {
        public void Configure(EntityTypeBuilder<LegalPerson> builder)
        {
        }
    }
}
