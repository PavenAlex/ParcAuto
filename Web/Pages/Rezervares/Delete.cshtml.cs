using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Rezervares
{
    public class DeleteModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public DeleteModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rezervare Rezervare { get; set; } = default!;

        public string NumePrenumeClient { get; set; } = string.Empty;
        public string MarcaModelVehicul { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervare = await _context.Rezervares.FirstOrDefaultAsync(m => m.ID_Rezervare == id);

            if (rezervare == null)
            {
                return NotFound();
            }
            else
            {
                Rezervare = rezervare;


                var client = await _context.Clients.FirstOrDefaultAsync(c => c.ID_Client == rezervare.ID_Client);
                if (client != null)
                {
                    NumePrenumeClient = $"{client.Nume} {client.Prenume}";
                }

                var vehicul = await _context.Vehicles.FirstOrDefaultAsync(v => v.ID_Vehicul == rezervare.ID_Vehicul);
                if (vehicul != null)
                {
                    MarcaModelVehicul = $"{vehicul.Marca} {vehicul.Model}";
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var facturaExistenta = await _context.Facturas
                .FirstOrDefaultAsync(f => f.ID_Rezervare == id);

            if (facturaExistenta != null)
            {

                ModelState.AddModelError(string.Empty, "Nu puteți șterge această rezervare deoarece există o factură asociată.");


                var rezervare = await _context.Rezervares.FirstOrDefaultAsync(m => m.ID_Rezervare == id);
                if (rezervare != null)
                {
                    Rezervare = rezervare;


                    var client = await _context.Clients.FirstOrDefaultAsync(c => c.ID_Client == rezervare.ID_Client);
                    if (client != null)
                    {
                        NumePrenumeClient = $"{client.Nume} {client.Prenume}";
                    }


                    var vehicul = await _context.Vehicles.FirstOrDefaultAsync(v => v.ID_Vehicul == rezervare.ID_Vehicul);
                    if (vehicul != null)
                    {
                        MarcaModelVehicul = $"{vehicul.Marca} {vehicul.Model}";
                    }
                }
                return Page(); 
            }

            var rezervareToDelete = await _context.Rezervares.FindAsync(id);
            if (rezervareToDelete != null)
            {
                Rezervare = rezervareToDelete;
                _context.Rezervares.Remove(Rezervare);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
