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


            var mentenantaAsociata = await _context.Mentenantas
                .FirstOrDefaultAsync(m => m.ID_Vehicul == vehicle.ID_Vehicul);


            var rezervareAsociata = await _context.Rezervares
                .FirstOrDefaultAsync(r => r.ID_Vehicul == vehicle.ID_Vehicul);

            if (mentenantaAsociata != null || rezervareAsociata != null)
            {

                ModelState.AddModelError(string.Empty, "Nu puteți șterge acest vehicul deoarece există o rezervare sau o mentenanță asociată.");
                Vehicle = vehicle; 
                return Page();
            }


            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
