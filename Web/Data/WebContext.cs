using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;

namespace Web.Data
{
    public class WebContext : DbContext
    {
        public WebContext (DbContextOptions<WebContext> options)
            : base(options)
        {
        }

        public DbSet<ParcAuto.Models.Vehicle> Vehicles { get; set; } = default!;
        public DbSet<ParcAuto.Models.Client> Clients { get; set; } = default!;
        public DbSet<ParcAuto.Models.Mentenanta> Mentenantas { get; set; } = default!;
        public DbSet<ParcAuto.Models.Rezervare> Rezervares { get; set; } = default!;
    }
}
