using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Clients
{
    public class EditModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public EditModel(Web.Data.WebContext context)
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
            Client = client;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validarea numărului de telefon (10 cifre, începe cu 0)
            if (Client.Telefon != null && !Regex.IsMatch(Client.Telefon, @"^0\d{9}$"))
            {
                ModelState.AddModelError("Client.Telefon", "Număr de telefon invalid");
            }

            // Validarea CNP-ului (13 cifre)
            if (Client.CNP != null && !Regex.IsMatch(Client.CNP, @"^\d{13}$"))
            {
                ModelState.AddModelError("Client.CNP", "CNP invalid");
            }

            // Validarea formatului de email
            if (Client.Email != null && !Regex.IsMatch(Client.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ModelState.AddModelError("Client.Email", "Mail invalid");
            }

            if (!ModelState.IsValid)
            {
                return Page(); // Dacă există erori de validare, întoarcem pagina cu mesajele de eroare
            }

            _context.Attach(Client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(Client.ID_Client))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ID_Client == id);
        }
    }
}
