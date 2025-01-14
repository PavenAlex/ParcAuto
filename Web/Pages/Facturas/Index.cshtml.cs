using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Facturas
{
    public class IndexModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public IndexModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        public IList<FacturaViewModel> Factura { get; set; } = default!;

        public class FacturaViewModel
        {
            public int ID_Factura { get; set; }
            public string ClientVehicul { get; set; } = string.Empty; 
            public DateTime Data_Emitere { get; set; }
            public decimal Suma_Totala { get; set; }
            public string Status_Plata { get; set; } = string.Empty;
        }

        public async Task OnGetAsync()
        {
            Factura = await _context.Facturas
                .Select(f => new FacturaViewModel
                {
                    ID_Factura = f.ID_Factura,
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
                            .FirstOrDefault(),
                    Data_Emitere = f.Data_Emitere,
                    Suma_Totala = f.Suma_Totala,
                    Status_Plata = f.Status_Plata
                })
                .ToListAsync();
        }
    }
}
