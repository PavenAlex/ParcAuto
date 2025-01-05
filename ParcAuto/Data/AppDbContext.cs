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
    }
}
