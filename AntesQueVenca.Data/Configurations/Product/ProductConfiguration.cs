using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntesQueVenca.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(prop => prop.ProductId);

            builder.HasOne(prop => prop.Category)
                .WithMany(prop => prop.Products)
                .HasForeignKey(prop => prop.CategoryId);

            builder.HasOne(u => u.CreatedBy)
                .WithMany(u => u.ProductsThatCreated)
                .HasForeignKey(u => u.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.LastModifiedBy)
                .WithMany(u => u.ProductsThatModified)
                .HasForeignKey(u => u.LastModifiedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
