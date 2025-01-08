using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Mentenantas
{
    public class DeleteModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public DeleteModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mentenanta Mentenanta { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentenanta = await _context.Mentenantas.FirstOrDefaultAsync(m => m.ID_Mentenanta == id);

            if (mentenanta == null)
            {
                return NotFound();
            }
            else
            {
                Mentenanta = mentenanta;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentenanta = await _context.Mentenantas.FindAsync(id);
            if (mentenanta != null)
            {
                Mentenanta = mentenanta;
                _context.Mentenantas.Remove(Mentenanta);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
