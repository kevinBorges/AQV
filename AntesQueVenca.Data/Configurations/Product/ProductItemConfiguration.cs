using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntesQueVenca.Data.Configurations
{
    public class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            builder.HasKey(prop => prop.ProductItemId);

            //builder.HasOne(prop => prop.Retailer)
            //    .WithMany(prop => prop.ProductItems)
            //    .HasForeignKey(prop => prop.RetailerId);

            builder.HasOne(prop => prop.Product)
                .WithMany(prop => prop.ProductItems)
                .HasForeignKey(prop => prop.ProductId);

            builder.HasOne(u => u.CreatedBy)
                     .WithMany(u => u.ProductItemsThatCreated)
                     .HasForeignKey(u => u.CreatedById)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.LastModifiedBy)
                .WithMany(u => u.ProductItemsThatModified)
                .HasForeignKey(u => u.LastModifiedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
