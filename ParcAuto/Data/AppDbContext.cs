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
                .HasOne(m => m.Vehicul) 
                .WithMany(v => v.Mentenante)
                .HasForeignKey(m => m.ID_Vehicul); 

            modelBuilder.Entity<Rezervare>()
               .HasOne(r => r.Factura)             
               .WithOne(f => f.Rezervare)          
               .HasForeignKey<Factura>(f => f.ID_Rezervare); 

           modelBuilder.Entity<Rezervare>()
                .HasOne(r => r.Client)                
                .WithMany(c => c.Rezervari)           
                .HasForeignKey(r => r.ID_Client);     

            modelBuilder.Entity<Rezervare>()
                .HasOne(r => r.Vehicle)              
                .WithMany(v => v.Rezervari)          
                .HasForeignKey(r => r.ID_Vehicul);     
        }

       
    }
}
