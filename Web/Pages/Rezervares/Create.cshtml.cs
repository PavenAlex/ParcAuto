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
            if (Rezervare.Data_Start < DateTime.Now.Date)
            {
                ModelState.AddModelError("Rezervare.Data_Start", "Data de început nu poate fi în trecut.");
            }


            // Validează dacă Data_Sfarsit este cel puțin la o zi după Data_Start
            if (Rezervare.Data_Sfarsit <= Rezervare.Data_Start.AddDays(1))
            {
                ModelState.AddModelError("Rezervare.Data_Sfarsit", "Data invalida");
            }

            if (!ModelState.IsValid)
            {
                // Repopulează dropdown-urile pentru a afișa din nou lista de clienți și vehicule
                Clients = new SelectList(
                    _context.Clients.Select(c => new
                    {
                        ID_Client = c.ID_Client,
                        NumeComplet = $"{c.Nume} {c.Prenume}"
                    }).ToList(),
                    "ID_Client",
                    "NumeComplet");

                Vehicles = new SelectList(
                    _context.Vehicles.Select(v => new
                    {
                        ID_Vehicul = v.ID_Vehicul,
                        NumeVehicul = $"{v.Marca} - {v.Model}"
                    }).ToList(),
                    "ID_Vehicul",
                    "NumeVehicul");

                return Page();
            }

            // Adaugă rezervarea în baza de date
            _context.Rezervares.Add(Rezervare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }


}
