using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntesQueVenca.Data.Configurations.Orders
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(prop => prop.OrderProductId);

            builder.HasOne(prop => prop.Order)
                .WithMany(prop => prop.OrderProducts)
                .HasForeignKey(prop => prop.OrderId);

            builder.HasOne(prop => prop.ProductItem)
                .WithMany(prop => prop.OrderProducts)
                .HasForeignKey(prop => prop.ProductItemId);

        }
    }
}
