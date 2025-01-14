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
    public class DeleteModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public DeleteModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FacturaViewModel Factura { get; set; } = default!;

        public class FacturaViewModel
        {
            public int ID_Factura { get; set; }
            public string ClientVehicul { get; set; } = string.Empty;
            public DateTime Data_Emitere { get; set; }
            public decimal Suma_Totala { get; set; }
            public string Status_Plata { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Where(f => f.ID_Factura == id)
                .Select(f => new FacturaViewModel
                {
                    ID_Factura = f.ID_Factura,
                    Data_Emitere = f.Data_Emitere,
                    Suma_Totala = f.Suma_Totala,
                    Status_Plata = f.Status_Plata,

                    ClientVehicul =
                        _context.Rezervares
                            .Where(r => r.ID_Rezervare == f.ID_Rezervare)
                            .Select(r =>
                                _context.Clients
                                    .Where(c => c.ID_Client == r.ID_Client)
                                    .Select(c => $"{c.Nume} {c.Prenume} - " +
                                                 _context.Vehicles
                                                     .Where(v => v.ID_Vehicul == r.ID_Vehicul)
                                                     .Select(v => $"{v.Marca} {v.Model}")
                                                     .FirstOrDefault())
                                    .FirstOrDefault())
                            .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (factura == null)
            {
                return NotFound();
            }
            else
            {
                Factura = factura; 
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
