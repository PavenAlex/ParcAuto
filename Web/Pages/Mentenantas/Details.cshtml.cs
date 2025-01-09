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
    public class DetailsModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public DetailsModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        public Mentenanta Mentenanta { get; set; } = default!;
        public string VehiculNume { get; set; } = string.Empty;  // Adăugăm o proprietate pentru numele vehiculului

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Căutăm mentenanța pe baza ID-ului
            var mentenanta = await _context.Mentenantas
                .FirstOrDefaultAsync(m => m.ID_Mentenanta == id);

            if (mentenanta == null)
            {
                return NotFound();
            }
            else
            {
                Mentenanta = mentenanta;

                // Căutăm vehiculul asociat
                var vehicul = await _context.Vehicles
                    .FirstOrDefaultAsync(v => v.ID_Vehicul == Mentenanta.ID_Vehicul);

                if (vehicul != null)
                {
                    // Creăm numele vehiculului
                    VehiculNume = $"{vehicul.Marca} {vehicul.Model}";
                }
            }
            return Page();
        }
    }
}
