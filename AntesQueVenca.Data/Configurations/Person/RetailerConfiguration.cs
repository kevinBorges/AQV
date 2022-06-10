using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntesQueVenca.Data.Configurations
{
    public class RetailerConfiguration : IEntityTypeConfiguration<Retailer>
    {
        public void Configure(EntityTypeBuilder<Retailer> builder)
        {
            builder.HasKey(p => p.RetailerId);

            builder.HasOne(p => p.Person)
                .WithMany(prop => prop.Retailers)
                .HasForeignKey(prop => prop.PersonId);  

            builder.HasOne(u => u.CreatedBy)
                .WithMany(u => u.RetailersThatCreated)
                .HasForeignKey(u => u.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.LastModifiedBy)
                .WithMany(u => u.RetailersThatModified)
                .HasForeignKey(u => u.LastModifiedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
