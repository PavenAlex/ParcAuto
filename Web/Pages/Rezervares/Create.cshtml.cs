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
        public SelectList StatusOptions { get; set; } // Dropdown pentru Status

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
                "NumeVehicul"
            );

            // Populează opțiunile pentru status
            StatusOptions = new SelectList(new List<string> { "în așteptare", "în curs", "finalizată" });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validare pentru Data_Start
            if (Rezervare.Data_Start < DateTime.Now.Date)
            {
                ModelState.AddModelError("Rezervare.Data_Start", "Data de început nu poate fi în trecut.");
            }

            // Validează dacă Data_Sfarsit este cel puțin la o zi după Data_Start
            if (Rezervare.Data_Sfarsit <= Rezervare.Data_Start.AddDays(1))
            {
                ModelState.AddModelError("Rezervare.Data_Sfarsit", "Data de sfârșit trebuie să fie cel puțin o zi după data de început.");
            }

            // Verificare suprapuneri rezervări existente
            var rezervariExistente = _context.Rezervares
                .Where(r => r.ID_Vehicul == Rezervare.ID_Vehicul)
                .ToList();

            foreach (var rezervare in rezervariExistente)
            {
                // Verificare suprapunere intervale
                if (Rezervare.Data_Start < rezervare.Data_Sfarsit && Rezervare.Data_Sfarsit > rezervare.Data_Start)
                {
                    ModelState.AddModelError("Rezervare.ID_Vehicul",
                        "Vehiculul selectat este deja rezervat în intervalul ales.");
                    break;
                }
            }

            if (!ModelState.IsValid)
            {
                // Repopulează dropdown-urile pentru a afișa din nou lista de clienți, vehicule și statusuri
                Clients = new SelectList(
                    _context.Clients.Select(c => new
                    {
                        ID_Client = c.ID_Client,
                        NumeComplet = $"{c.Nume} {c.Prenume}"
                    }).ToList(),
                    "ID_Client",
                    "NumeComplet"
                );

                Vehicles = new SelectList(
                    _context.Vehicles.Select(v => new
                    {
                        ID_Vehicul = v.ID_Vehicul,
                        NumeVehicul = $"{v.Marca} - {v.Model}"
                    }).ToList(),
                    "ID_Vehicul",
                    "NumeVehicul"
                );

                StatusOptions = new SelectList(new List<string> { "în așteptare", "în curs", "finalizată" });

                return Page();
            }

            // Adaugă rezervarea în baza de date
            _context.Rezervares.Add(Rezervare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
