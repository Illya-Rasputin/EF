using Intro_1.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro_1
{
    public class GameDbContext : DbContext
    {
        public DbSet<GameEntity> Games { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=GameDb;Trusted_Connection=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<GameEntity>()
                .HasKey(g => g.Id);

            builder.Entity<GameEntity>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Entity<GameEntity>()
                .Property(g => g.PlayMode)
                .HasDefaultValue("Single Player");

            builder.Entity<GameEntity>()
                .Property(g => g.Developer)
                .HasMaxLength(100);

            builder.Entity<GameEntity>()
                .Property(g => g.Copies)
                .HasDefaultValue(0);
        }
    }
}
