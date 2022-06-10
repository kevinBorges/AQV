using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntesQueVenca.Data.Configurations
{
    public class RetailerProductItemConfiguration : IEntityTypeConfiguration<RetailerProductItem>
    {
        public void Configure(EntityTypeBuilder<RetailerProductItem> builder)
        {
            builder.HasKey(prop => prop.RetailerProductItemId);

            builder.HasOne(prop => prop.Retailer)
                .WithMany(prop => prop.RetailerProducts)
                .HasForeignKey(prop => prop.RetailerId);

            builder.HasOne(prop => prop.ProductItem)
                .WithMany(prop => prop.RetailerProducts)
                .HasForeignKey(prop => prop.ProductItemId);
        }
    }
}
