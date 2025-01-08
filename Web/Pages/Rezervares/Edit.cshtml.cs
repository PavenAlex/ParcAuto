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

namespace Web.Pages.Rezervares
{
    public class EditModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public EditModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rezervare Rezervare { get; set; } = default!;

        public string ClientNume { get; set; } = string.Empty;  // Adăugăm o proprietate pentru numele clientului
        public string VehiculNume { get; set; } = string.Empty; // Adăugăm o proprietate pentru marca vehiculului

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Căutăm rezervarea
            var rezervare = await _context.Rezervares
                .FirstOrDefaultAsync(r => r.ID_Rezervare == id);

            if (rezervare == null)
            {
                return NotFound();
            }

            // Setăm rezervarea în model
            Rezervare = rezervare;

            // Căutăm clientul asociat
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.ID_Client == Rezervare.ID_Client);

            if (client != null)
            {
                // Construim numele clientului
                ClientNume = $"{client.Nume} {client.Prenume}";
            }

            // Căutăm vehiculul asociat
            var vehicul = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.ID_Vehicul == Rezervare.ID_Vehicul);

            if (vehicul != null)
            {
                // Construim marca vehiculului
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

            _context.Attach(Rezervare).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RezervareExists(Rezervare.ID_Rezervare))
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

        private bool RezervareExists(int id)
        {
            return _context.Rezervares.Any(e => e.ID_Rezervare == id);
        }
    }

}
