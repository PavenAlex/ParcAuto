using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Clients
{
    public class DeleteModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public DeleteModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Client Client { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FirstOrDefaultAsync(m => m.ID_Client == id);

            if (client == null)
            {
                return NotFound();
            }
            else
            {
                Client = client;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Verificăm dacă clientul se află în tabelul Rezervari
            var rezervari = await _context.Rezervares
                .Where(r => r.ID_Client == id)
                .ToListAsync();

            if (rezervari.Any())
            {
                // Dacă există rezervări asociate, adăugăm un mesaj de eroare
                ModelState.AddModelError(string.Empty, "Nu puteți șterge acest client deoarece există rezervări asociate.");

                // Încarcăm din nou clientul în caz de eroare
                var client = await _context.Clients.FirstOrDefaultAsync(m => m.ID_Client == id);
                if (client != null)
                {
                    Client = client;
                }

                // Rămânem pe aceleași pagină pentru a afișa eroarea și datele clientului
                return Page();
            }

            var clientToDelete = await _context.Clients.FindAsync(id);
            if (clientToDelete != null)
            {
                Client = clientToDelete;
                _context.Clients.Remove(Client);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
