using AntesQueVenca.Data.Configurations;
using AntesQueVenca.Data.Configurations.Orders;
using AntesQueVenca.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace AntesQueVenca.Data.Context
{
    public class AntesQueVencaContext : IdentityDbContext
    {
        public AntesQueVencaContext()
        {
                
        }

        public AntesQueVencaContext(DbContextOptions<AntesQueVencaContext> options):base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<PhysicalPerson> PhysicalPerson { get; set; }

        public DbSet<LegalPerson> LegalPerson { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductItem> ProductItem { get; set; }

        public DbSet<OrderProduct> OrderProduct { get; set; }

        public DbSet<Retailer> Retailer { get; set; }

        public DbSet<RetailerProductItem> RetailerProductItem { get; set; }

        public DbSet<PostalCodeDetail> PostalCodeDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PhysicalPersonConfiguration());
            modelBuilder.ApplyConfiguration(new LegalPersonConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
            modelBuilder.ApplyConfiguration(new RetailerConfiguration());
            modelBuilder.ApplyConfiguration(new RetailerProductItemConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("AntesQueVencaDatabase");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedDate") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("CreatedDate").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("CreatedDate").IsModified = false;
            }

            return base.SaveChanges();
        }
    }
}
