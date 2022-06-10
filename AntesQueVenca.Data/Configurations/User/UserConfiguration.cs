using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AntesQueVenca.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
        }
    }
}