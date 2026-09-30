using DataEntity.Data;
using Microsoft.EntityFrameworkCore;

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
                        "Server=localhost;" +
                        "Database=SkladCv2_2026;" +
                        "User Id=sa;" +
                        "Password=123456;" +
                        "TrustServerCertificate=True;")
                    .UseLazyLoadingProxies();
            }
        }

    }
}
