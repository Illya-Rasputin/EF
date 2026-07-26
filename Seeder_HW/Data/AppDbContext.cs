using Microsoft.EntityFrameworkCore;
using Seeder_HW.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seeder_HW.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<OrderEntity> Orders { get; set; } = null!;
        public DbSet<OrderItemEntity> OrderItems { get; set; } = null!;
        public DbSet<GameEntity> Games { get; set; } = null!;
        public DbSet<DeveloperEntity> Developers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=GameShop;Trusted_Connection=True;TrustServerCertificate=True;";
            optionsBuilder
               .UseLazyLoadingProxies()
               .UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GameEntity>(entity =>
              {
                  entity.HasKey(g => g.Id);
                  entity.Property(g => g.Name);
                  entity.Property(g => g.Price);
                  entity.Property(g => g.ReleaseYear);   
              }
            );

            builder.Entity<DeveloperEntity>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name);
                entity.Property(d => d.Country);
            }
            );

            builder.Entity<CustomerEntity>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.FullName);
                entity.Property(d => d.Email);
            }
            );

            builder.Entity<OrderEntity>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.OrderDate);
            }
            );

            builder.Entity<OrderItemEntity>(entity =>
            {
                entity.HasKey(d => d.Id);
            }
            );



            builder.Entity<DeveloperEntity>()
                .HasMany(d => d.Games)
                .WithOne(g => g.Developer)
                .HasForeignKey(g => g.DeveloperId);

            builder.Entity<GameEntity>()
                .HasOne(g => g.Developer)
                .WithMany(d => d.Games)
                .HasForeignKey(g => g.DeveloperId);

            builder.Entity<OrderEntity>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            builder.Entity<OrderItemEntity>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            builder.Entity<OrderItemEntity>()
                .HasOne(oi => oi.Game)
                .WithMany(g => g.OrderItems)
                .HasForeignKey(oi => oi.GameId);
        } 
    }
}