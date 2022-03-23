using Microsoft.EntityFrameworkCore;

namespace Models{
    public class AerodromContext : DbContext{
        public DbSet<Aerodrom> Aerodromi { get; set; }
        public DbSet<Destinacija> Destinacije { get; set; }
        public DbSet<Let> Letovi { get; set; }
        public DbSet<Sediste> Sedista { get; set; }

        public DbSet<Putnik> Putnici { get; set; }

        public AerodromContext(DbContextOptions options) : base(options)
        {

        }

    }

}
