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

        public void Seed()
        {
            // Kontrola, zda již v DB nejsou data, aby se zabránilo duplicitám.
            if (MernaJednotka.Any())
            {
                return; // Databáze již byla naplněna.
            }

            // Vytvoření měrných jednotek
            var mjKs = new MernaJednotka { Popis = "ks" };
            var mjKg = new MernaJednotka { Popis = "kg" };
            var mjM3 = new MernaJednotka { Popis = "m3" };


            // Přidání všech nových objektů do kontextu
            MernaJednotka.AddRange(mjKs, mjKg, mjM3);

            // Jediné finální uložení všech změn do databáze
            SaveChanges();
        }


    }
}
