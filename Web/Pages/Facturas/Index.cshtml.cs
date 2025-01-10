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

        // Lista de facturi cu detalii suplimentare despre client și vehicul
        public IList<FacturaViewModel> Factura { get; set; } = default!;

        // Clasa ViewModel personalizată
        public class FacturaViewModel
        {
            public int ID_Factura { get; set; }
            public string ClientVehicul { get; set; } = string.Empty; // Detalii despre client și vehicul
            public DateTime Data_Emitere { get; set; }
            public decimal Suma_Totala { get; set; }
            public string Status_Plata { get; set; } = string.Empty;
        }

        public async Task OnGetAsync()
        {
            // Obținem toate facturile și adăugăm manual informațiile despre client și vehicul
            Factura = await _context.Facturas
                .Select(f => new FacturaViewModel
                {
                    ID_Factura = f.ID_Factura,
                    // Căutăm rezervarea asociată facturii pentru a obține ID_Client și ID_Vehicul
                    ClientVehicul =
                        // Căutăm rezervarea pe baza ID-ului de rezervare
                        _context.Rezervares
                            .Where(r => r.ID_Rezervare == f.ID_Rezervare)
                            .Select(r =>
                                // Căutăm clientul pe baza ID-ului clientului din rezervare
                                _context.Clients
                                    .Where(c => c.ID_Client == r.ID_Client)
                                    .Select(c => $"{c.Nume} {c.Prenume} - " +
                                                 // Căutăm vehiculul pe baza ID-ului vehiculului din rezervare
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
