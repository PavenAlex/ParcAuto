using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParcAuto.Models;
using Web.Data;

namespace Web.Pages.Mentenantas
{
    public class CreateModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public CreateModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        // Lista de vehicule pentru dropdown
        public List<SelectListItem> Vehicule { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public Mentenanta Mentenanta { get; set; } = default!;

        public IActionResult OnGet()
        {
            // Populează lista de vehicule
            Vehicule = _context.Vehicles
                               .Select(v => new SelectListItem
                               {
                                   Value = v.ID_Vehicul.ToString(),
                                   Text = $"{v.Marca} {v.Model}"
                               }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Refacem lista vehiculelor dacă există erori de validare
                Vehicule = _context.Vehicles
                                   .Select(v => new SelectListItem
                                   {
                                       Value = v.ID_Vehicul.ToString(),
                                       Text = $"{v.Marca} {v.Model}"
                                   }).ToList();
                return Page();
            }

            _context.Mentenantas.Add(Mentenanta);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
