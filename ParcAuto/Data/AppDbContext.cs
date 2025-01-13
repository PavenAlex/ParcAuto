using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;

namespace ParcAuto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Rezervare> Rezervares { get; set; }
        public DbSet<Mentenanta> Mentenantas { get; set; }
        public DbSet<Factura> Facturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mentenanta>()
                .HasOne(m => m.Vehicul) // Relația 1-N
                .WithMany(v => v.Mentenante)
                .HasForeignKey(m => m.ID_Vehicul); // Cheia străină

            modelBuilder.Entity<Rezervare>()
               .HasOne(r => r.Factura)               // O Rezervare are o Factura
               .WithOne(f => f.Rezervare)            // O Factura are o Rezervare
               .HasForeignKey<Factura>(f => f.ID_Rezervare); // Cheia străină este ID_Rezervare în Factura
        }

       
    }
}
