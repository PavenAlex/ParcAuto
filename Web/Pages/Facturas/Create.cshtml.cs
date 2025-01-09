using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Facturas
{
    public class CreateModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public CreateModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        public SelectList RezervariDropdown { get; set; }

        public IActionResult OnGet()
        {
            // Populează lista cu rezervări folosind interogări manuale pentru a asocia clienți și vehicule
            var rezervariData = _context.Rezervares
                .Join(
                    _context.Clients,
                    rezervare => rezervare.ID_Client,  // Cheia din tabelul Rezervares
                    client => client.ID_Client,       // Cheia din tabelul Clients
                    (rezervare, client) => new
                    {
                        rezervare.ID_Rezervare,
                        rezervare.Data_Start,
                        ClientNume = $"{client.Nume} {client.Prenume}",
                        rezervare.ID_Vehicul
                    }
                )
                .Join(
                    _context.Vehicles,
                    rezervareClient => rezervareClient.ID_Vehicul,  // Cheia din rezultatul anterior
                    vehicul => vehicul.ID_Vehicul,                 // Cheia din tabelul Vehicles
                    (rezervareClient, vehicul) => new
                    {
                        rezervareClient.ID_Rezervare,
                        DisplayInfo = $"{rezervareClient.Data_Start:yyyy-MM-dd} - {rezervareClient.ClientNume} - {vehicul.Marca} {vehicul.Model}"
                    }
                )
                .ToList();

            // Creează un SelectList pentru dropdown
            RezervariDropdown = new SelectList(
                rezervariData,
                "ID_Rezervare",
                "DisplayInfo"
            );

            return Page();
        }

        [BindProperty]
        public Factura Factura { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Re-populați dropdown-ul în caz de eroare de validare
                OnGet();
                return Page();
            }

            // Verifică dacă există deja o factură pentru această rezervare
            var existingFactura = await _context.Facturas
                .FirstOrDefaultAsync(f => f.ID_Rezervare == Factura.ID_Rezervare);

            if (existingFactura != null)
            {
                // Dacă există, adaugă un mesaj de eroare
                ModelState.AddModelError("Factura.ID_Rezervare", "Există deja o factură pentru această rezervare.");
                OnGet(); // Re-populăm dropdown-ul cu datele corecte
                return Page();
            }

            // Obțineți data de start a rezervării selectate
            var rezervare = await _context.Rezervares
                .FirstOrDefaultAsync(r => r.ID_Rezervare == Factura.ID_Rezervare);

            if (rezervare == null)
            {
                ModelState.AddModelError("Factura.ID_Rezervare", "Rezervarea selectată nu există.");
                OnGet();
                return Page();
            }

            // Setează Data_Emitere să fie aceeași cu Data_Start
            Factura.Data_Emitere = rezervare.Data_Start;

            // Adaugă factura și salvează
            _context.Facturas.Add(Factura);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
