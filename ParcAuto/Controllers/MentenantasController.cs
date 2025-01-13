using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcAuto.Data;
using ParcAuto.Models;

namespace ParcAuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentenantasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MentenantasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mentenanta>>> GetMentenantas()
        {
            return await _context.Mentenantas.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Mentenanta>> GetMentenanta(int id)
        {
            var mentenanta = await _context.Mentenantas.FindAsync(id);

            if (mentenanta == null)
            {
                return NotFound();
            }

            return mentenanta;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutMentenanta(int id, Mentenanta mentenanta)
        {
            if (id != mentenanta.ID_Mentenanta)
            {
                return BadRequest();
            }

            _context.Entry(mentenanta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MentenantaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Mentenanta>> PostMentenanta(Mentenanta mentenanta)
        {
            _context.Mentenantas.Add(mentenanta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMentenanta", new { id = mentenanta.ID_Mentenanta }, mentenanta);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentenanta(int id)
        {
            var mentenanta = await _context.Mentenantas.FindAsync(id);
            if (mentenanta == null)
            {
                return NotFound();
            }

            _context.Mentenantas.Remove(mentenanta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MentenantaExists(int id)
        {
            return _context.Mentenantas.Any(e => e.ID_Mentenanta == id);
        }
    }
}
