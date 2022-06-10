using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntesQueVenca.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasOne(u => u.CreatedBy)
                .WithMany(u => u.PersonsThatCreated)
                .HasForeignKey(u => u.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.LastModifiedBy)
                .WithMany(u => u.PersonsThatModified)
                .HasForeignKey(u => u.LastModifiedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
