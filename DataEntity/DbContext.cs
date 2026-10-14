using DataEntity.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataEntity
{
    // Classu pojmenovat : DbContext - Dědí z Microsoft.EntityFrameworkCore.DbContext
    public class SkladContext : DbContext
    {
        public DbSet<Material> Material => Set<Material>();
        public DbSet<MernaJednotka> MernaJednotka => Set<MernaJednotka>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseSqlServer(
                        "Data Source=(localdb)\\MSSQLLocalDB;" +
                        "Database=SkladCv2_2026;" +
                        "Integrated Security=True;" +
                        "TrustServerCertificate=True;")
                    .UseLazyLoadingProxies();
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MernaJednotka>().HasData(
                new MernaJednotka { MernaJednotkaId = 1, Popis = "ks" },
                new MernaJednotka { MernaJednotkaId = 2, Popis = "kg" },
                new MernaJednotka { MernaJednotkaId = 3, Popis = "m3" });
        }
    }
}
