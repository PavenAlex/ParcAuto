using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Mentenantas
{
    public class IndexModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public IndexModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        public IList<Mentenanta> Mentenanta { get; set; } = default!;
        public Dictionary<int, string> Vehicule { get; set; } = new Dictionary<int, string>();

        public async Task OnGetAsync()
        {
            // Obține lista de mentenanțe
            Mentenanta = await _context.Mentenantas.ToListAsync();

            // Obține vehiculele și creează un dicționar cu ID_Vehicul -> "Marca Model"
            Vehicule = await _context.Vehicles
                                    .ToDictionaryAsync(v => v.ID_Vehicul, v => $"{v.Marca} {v.Model}");

        }
    }

}
