using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntesQueVenca.Data.Configurations.Orders
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(prop => prop.OrderId);

            builder.HasOne(prop => prop.Retailer)
                .WithMany(prop => prop.Orders)
                .HasForeignKey(prop => prop.RetailerId);

            builder.HasOne(u => u.CreatedBy)
                 .WithMany(u => u.OrdersThatCreated)
                 .HasForeignKey(u => u.CreatedById)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.LastModifiedBy)
                .WithMany(u => u.OrdersThatModified)
                .HasForeignKey(u => u.LastModifiedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
