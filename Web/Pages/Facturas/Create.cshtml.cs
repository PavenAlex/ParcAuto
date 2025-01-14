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
            var rezervariData = _context.Rezervares
                .Join(
                    _context.Clients,
                    rezervare => rezervare.ID_Client,  
                    client => client.ID_Client,       
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
                    rezervareClient => rezervareClient.ID_Vehicul,  
                    vehicul => vehicul.ID_Vehicul,                 
                    (rezervareClient, vehicul) => new
                    {
                        rezervareClient.ID_Rezervare,
                        DisplayInfo = $"{rezervareClient.Data_Start:yyyy-MM-dd} - {rezervareClient.ClientNume} - {vehicul.Marca} {vehicul.Model}"
                    }
                )
                .ToList();

            RezervariDropdown = new SelectList(
                rezervariData,
                "ID_Rezervare",
                "DisplayInfo"
            );

            return Page();
        }

        [BindProperty]
        public Factura Factura { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }


            var existingFactura = await _context.Facturas
                .FirstOrDefaultAsync(f => f.ID_Rezervare == Factura.ID_Rezervare);

            if (existingFactura != null)
            {
                ModelState.AddModelError("Factura.ID_Rezervare", "Există deja o factură pentru această rezervare.");
                OnGet(); 
                return Page();
            }


            var rezervare = await _context.Rezervares
                .FirstOrDefaultAsync(r => r.ID_Rezervare == Factura.ID_Rezervare);

            if (rezervare == null)
            {
                ModelState.AddModelError("Factura.ID_Rezervare", "Rezervarea selectată nu există.");
                OnGet();
                return Page();
            }


            Factura.Data_Emitere = rezervare.Data_Start;

            _context.Facturas.Add(Factura);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
