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


        public SelectList Clients { get; set; }
        public SelectList Vehicles { get; set; }
        public SelectList StatusOptions { get; set; } 

        [BindProperty]
        public Rezervare Rezervare { get; set; } = default!;

        public IActionResult OnGet()
        {
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (Rezervare.Data_Start < DateTime.Now.Date)
            {
                ModelState.AddModelError("Rezervare.Data_Start", "Data de început nu poate fi în trecut.");
            }


            if (Rezervare.Data_Sfarsit <= Rezervare.Data_Start.AddDays(1))
            {
                ModelState.AddModelError("Rezervare.Data_Sfarsit", "Data de sfârșit trebuie să fie cel puțin o zi după data de început.");
            }

            var rezervariExistente = _context.Rezervares
                .Where(r => r.ID_Vehicul == Rezervare.ID_Vehicul)
                .ToList();

            foreach (var rezervare in rezervariExistente)
            {
                if (Rezervare.Data_Start < rezervare.Data_Sfarsit && Rezervare.Data_Sfarsit > rezervare.Data_Start)
                {
                    ModelState.AddModelError("Rezervare.ID_Vehicul",
                        "Vehiculul selectat este deja rezervat în intervalul ales.");
                    break;
                }
            }

            if (!ModelState.IsValid)
            {
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


            _context.Rezervares.Add(Rezervare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
