using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntesQueVenca.Data.Configurations
{
    public class PhysicalPersonConfiguration : IEntityTypeConfiguration<PhysicalPerson>
    {
        public void Configure(EntityTypeBuilder<PhysicalPerson> builder)
        {
        }
    }
}
