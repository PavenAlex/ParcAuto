using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Vehicles
{
    public class DeleteModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public DeleteModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(m => m.ID_Vehicul == id);

            if (vehicle == null)
            {
                return NotFound();
            }
            else
            {
                Vehicle = vehicle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            // Verifică dacă există o mentenanță asociată
            var mentenantaAsociata = await _context.Mentenantas
                .FirstOrDefaultAsync(m => m.ID_Vehicul == vehicle.ID_Vehicul);

            // Verifică dacă există o rezervare asociată
            var rezervareAsociata = await _context.Rezervares
                .FirstOrDefaultAsync(r => r.ID_Vehicul == vehicle.ID_Vehicul);

            if (mentenantaAsociata != null || rezervareAsociata != null)
            {
                // Dacă există mentenanță sau rezervare asociată, adaugă un mesaj de eroare și nu permite ștergerea
                ModelState.AddModelError(string.Empty, "Nu puteți șterge acest vehicul deoarece există o rezervare sau o mentenanță asociată.");
                Vehicle = vehicle; // Păstrează detaliile vehiculului pentru a le afisa în view
                return Page();
            }

            // Dacă nu există mentenanță sau rezervare asociată, șterge vehiculul
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
