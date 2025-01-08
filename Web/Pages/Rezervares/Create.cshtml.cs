using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Rezervares
{
    public class CreateModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public CreateModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        // Liste pentru a popula dropdown-urile
        public SelectList Clients { get; set; }
        public SelectList Vehicles { get; set; }

        [BindProperty]
        public Rezervare Rezervare { get; set; } = default!;

        public IActionResult OnGet()
        {
            // Populează lista cu clienți
            Clients = new SelectList(
                _context.Clients.Select(c => new
                {
                    ID_Client = c.ID_Client,
                    NumeComplet = $"{c.Nume} {c.Prenume}" // Concatenarea numelui și prenumelui clientului
                }).ToList(),
                "ID_Client",
                "NumeComplet"
);

            // Populează lista cu vehicule
            Vehicles = new SelectList(
                _context.Vehicles.Select(v => new
                {
                    ID_Vehicul = v.ID_Vehicul,
                    NumeVehicul = $"{v.Marca} - {v.Model}" // Concatenarea mărcii și modelului
                }).ToList(),
                "ID_Vehicul",
                "NumeVehicul");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Adaugă rezervarea în baza de date
            _context.Rezervares.Add(Rezervare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }


}
