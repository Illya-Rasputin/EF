using Intro_2.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro_2
{
    public class TeamDbContext: DbContext
    {
        public DbSet<TeamEntity> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TeamDb;Trusted_Connection=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<TeamEntity>()
                .HasKey(g => g.Id);

            builder.Entity<TeamEntity>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Entity<TeamEntity>()
                .Property(g => g.City)
                .HasMaxLength(100);

            builder.Entity<TeamEntity>()
                .Property(g => g.Wins)
                .HasDefaultValue(0);

            builder.Entity<TeamEntity>()
                .Property(g => g.Losses)
                .HasDefaultValue(0);

            builder.Entity<TeamEntity>()
                .Property(g => g.Draws)
                .HasDefaultValue(0);

            builder.Entity<TeamEntity>()
                .Property(g => g.Scores)
                .HasDefaultValue(0);

            builder.Entity<TeamEntity>()
                .Property(g => g.ScoredOn)
                .HasDefaultValue(0);
        }
    }
}
