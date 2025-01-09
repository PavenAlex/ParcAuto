using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Facturas
{
    public class EditModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public EditModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Factura Factura { get; set; } = default!;

        public string ClientName { get; set; } = string.Empty;
        public string VehicleDetails { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FirstOrDefaultAsync(m => m.ID_Factura == id);
            if (factura == null)
            {
                return NotFound();
            }

            Factura = factura;

            // Obține detalii pentru client și vehicul
            var rezervare = await _context.Rezervares.FirstOrDefaultAsync(r => r.ID_Rezervare == Factura.ID_Rezervare);
            if (rezervare != null)
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.ID_Client == rezervare.ID_Client);
                if (client != null)
                {
                    ClientName = $"{client.Nume} {client.Prenume}";
                }

                var vehicul = await _context.Vehicles.FirstOrDefaultAsync(v => v.ID_Vehicul == rezervare.ID_Vehicul);
                if (vehicul != null)
                {
                    VehicleDetails = $"{vehicul.Marca} {vehicul.Model}";
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Actualizează doar Status_Plata
            var facturaToUpdate = await _context.Facturas.FirstOrDefaultAsync(f => f.ID_Factura == Factura.ID_Factura);
            if (facturaToUpdate == null)
            {
                return NotFound();
            }

            facturaToUpdate.Status_Plata = Factura.Status_Plata;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
