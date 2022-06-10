using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntesQueVenca.Data.Configurations
{
   public  class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasOne(u => u.CreatedBy)
                .WithMany(u => u.CategoryThatCreated)
                .HasForeignKey(u => u.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.LastModifiedBy)
                .WithMany(u => u.CategoryThatModified)
                .HasForeignKey(u => u.LastModifiedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
