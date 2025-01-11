using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Models;
using Web.Data;

namespace Web.Vehicles
{
    public class DetailsModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public DetailsModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        public Vehicle Vehicle { get; set; } = default!;
        public List<Rezervare> Rezervari { get; set; } = new List<Rezervare>(); // Lista rezervărilor pentru vehicul

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(m => m.ID_Vehicul == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            // Obține doar rezervările viitoare sau cele care se termină astăzi
            Rezervari = await _context.Rezervares
                .Where(r => r.ID_Vehicul == id && r.Data_Sfarsit >= DateTime.Now)
                .ToListAsync();

            Vehicle = vehicle;
            return Page();
        }
    }
}
