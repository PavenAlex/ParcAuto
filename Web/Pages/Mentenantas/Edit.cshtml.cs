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
        public string VehiculNume { get; set; } = string.Empty;  

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentenanta = await _context.Mentenantas
                .FirstOrDefaultAsync(m => m.ID_Mentenanta == id);

            if (mentenanta == null)
            {
                return NotFound();
            }

            Mentenanta = mentenanta;

            var vehicul = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.ID_Vehicul == Mentenanta.ID_Vehicul);

            if (vehicul != null)
            {
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
