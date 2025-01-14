using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Rezervares
{
    public class IndexModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public IndexModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        public IList<Rezervare> Rezervare { get; set; } = default!;
        public Dictionary<int, string> Clients { get; set; } = new Dictionary<int, string>();  
        public Dictionary<int, string> Vehicles { get; set; } = new Dictionary<int, string>(); 

        public async Task OnGetAsync()
        {
            Rezervare = await _context.Rezervares.ToListAsync();

            Clients = await _context.Clients
                                    .ToDictionaryAsync(c => c.ID_Client, c => $"{c.Nume} {c.Prenume}");
            Vehicles = await _context.Vehicles
                                    .ToDictionaryAsync(v => v.ID_Vehicul, v => $"{v.Marca} {v.Model}");
        }
    }
}
