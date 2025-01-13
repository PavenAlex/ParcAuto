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
    public class FacturasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacturasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            return await _context.Facturas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null)
            {
                return NotFound();
            }

            return factura;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(int id, Factura factura)
        {
            if (id != factura.ID_Factura)
            {
                return BadRequest();
            }

            _context.Entry(factura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
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
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFactura", new { id = factura.ID_Factura }, factura);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.ID_Factura == id);
        }
    }
}
