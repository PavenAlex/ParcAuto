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

        public string ClientNume { get; set; } = string.Empty;  
        public string VehiculNume { get; set; } = string.Empty; 

        public SelectList StatusOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var rezervare = await _context.Rezervares
                .FirstOrDefaultAsync(r => r.ID_Rezervare == id);

            if (rezervare == null)
            {
                return NotFound();
            }


            Rezervare = rezervare;

            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.ID_Client == Rezervare.ID_Client);

            if (client != null)
            {
                ClientNume = $"{client.Nume} {client.Prenume}";
            }


            var vehicul = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.ID_Vehicul == Rezervare.ID_Vehicul);

            if (vehicul != null)
            {

                VehiculNume = $"{vehicul.Marca} {vehicul.Model}";
            }


            StatusOptions = new SelectList(new List<string> { "în așteptare", "în curs", "finalizată" });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Rezervare.Data_Start < DateTime.Now.Date)
            {
                ModelState.AddModelError("Rezervare.Data_Start", "Data de început nu poate fi în trecut.");
            }


            if (Rezervare.Data_Sfarsit <= Rezervare.Data_Start.AddDays(1))
            {
                ModelState.AddModelError("Rezervare.Data_Sfarsit", "Data invalidă.");
            }

            if (!ModelState.IsValid)
            {

                StatusOptions = new SelectList(new List<string> { "în așteptare", "în curs", "finalizată" });
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
