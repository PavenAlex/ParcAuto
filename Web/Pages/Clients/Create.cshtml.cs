using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public CreateModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Client Client { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Client.Telefon != null && !Regex.IsMatch(Client.Telefon, @"^0\d{9}$"))
            {
                ModelState.AddModelError("Client.Telefon", "Număr de telefon invalid");
            }

            if (Client.CNP != null && !Regex.IsMatch(Client.CNP, @"^\d{13}$"))
            {
                ModelState.AddModelError("Client.CNP", "CNP invalid");
            }

            if (Client.Email != null && !Regex.IsMatch(Client.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                ModelState.AddModelError("Client.Email", "Mail invalid");
            }

            var existingPhone = await _context.Clients
                .Where(c => c.Telefon == Client.Telefon)
                .FirstOrDefaultAsync();

            if (existingPhone != null)
            {
                ModelState.AddModelError("Client.Telefon", "Numărul de telefon există deja.");
            }

            
            var existingEmail = await _context.Clients
                .Where(c => c.Email == Client.Email)
                .FirstOrDefaultAsync();

            if (existingEmail != null)
            {
                ModelState.AddModelError("Client.Email", "Adresa de email există deja.");
            }

            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            _context.Clients.Add(Client);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
