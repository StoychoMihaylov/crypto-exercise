using Database.Entities;
using Database.interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database.Context
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        public ApplicationDBContext(DbContextOptions options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<PortfolioEntry> PortfolioEntries { get; set; }

        public DbSet<CoinValue> CoinValues { get; set; }

        public DbSet<PortfolioValue> PortfolioValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("citext");

            // Needed for timescaledb
            modelBuilder.HasPostgresExtension("timescaledb");

            // Also needed for timescaledb, PK has to include time column
            modelBuilder.Entity<CoinValue>()
                .HasKey(e => new { e.Id, e.Time });

            modelBuilder.Entity<PortfolioValue>()
                .HasKey(e => new { e.Id, e.Time });

            modelBuilder.Entity<CoinValue>()
                .Property(e => e.CreatedOn)
                .HasDefaultValue(DateTime.UtcNow);

            modelBuilder.Entity<PortfolioValue>()
             .Property(e => e.CreatedOn)
             .HasDefaultValue(DateTime.UtcNow);


            // Test data (modify it if you want)
            //modelBuilder.Entity<User>()
            //    .HasData(new User
            //    {
            //        Id = 1,
            //        UserName = "wilson"
            //    });

            //modelBuilder.Entity<PortfolioEntry>()
            //    .HasData(new PortfolioEntry
            //    {
            //        Id = 1,
            //        UserId = 1,
            //        Coin = "BTC",
            //        Quantitiy = 0.005M
            //    });

            //modelBuilder.Entity<PortfolioEntry>()
            //    .HasData(new PortfolioEntry
            //    {
            //        Id = 2,
            //        UserId = 1,
            //        Coin = "ETH",
            //        Quantitiy = 0.05M
            //    });

            //modelBuilder.Entity<PortfolioEntry>()
            //    .HasData(new PortfolioEntry
            //    {
            //        Id = 3,
            //        UserId = 1,
            //        Coin = "ADA",
            //        Quantitiy = 30M
            //    });

            modelBuilder.Entity<PortfolioEntry>()
                .HasIndex(e => new { e.Coin, e.UserId })
                .IsUnique();

            modelBuilder.Entity<PortfolioEntry>()
               .HasIndex(e => new { e.Coin, e.UserId })
               .IsUnique();
        }
    }
}
