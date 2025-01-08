using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Mentenantas
{
    public class EditModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public EditModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mentenanta Mentenanta { get; set; } = default!;
        public string VehiculNume { get; set; } = string.Empty;  // Adăugăm o proprietate pentru numele vehiculului

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Căutăm mentenanța
            var mentenanta = await _context.Mentenantas
                .FirstOrDefaultAsync(m => m.ID_Mentenanta == id);

            if (mentenanta == null)
            {
                return NotFound();
            }

            // Setăm mentenanța în model
            Mentenanta = mentenanta;

            // Căutăm vehiculul asociat
            var vehicul = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.ID_Vehicul == Mentenanta.ID_Vehicul);

            if (vehicul != null)
            {
                // Construim numele vehiculului
                VehiculNume = $"{vehicul.Marca} {vehicul.Model}";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Actualizăm doar câmpurile permise: Tip_Interventie, Data_Programare și Cost_Estimativ
            _context.Attach(Mentenanta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MentenantaExists(Mentenanta.ID_Mentenanta))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MentenantaExists(int id)
        {
            return _context.Mentenantas.Any(e => e.ID_Mentenanta == id);
        }
    }

}
