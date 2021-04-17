using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FestivalAPI.Data;
using FestivalAPI.Models;

namespace FestivalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifsController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public TarifsController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Tarifs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarif>>> GetTarif()
        {
            return await _context.Tarif.ToListAsync();
        }

        // GET: api/Tarifs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarif>> GetTarif(int id)
        {
            var tarif = await _context.Tarif.FindAsync(id);

            if (tarif == null)
            {
                return NotFound();
            }

            return tarif;
        }

        // PUT: api/Tarifs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarif(int id, Tarif tarif)
        {
            if (id != tarif.IdT)
            {
                return BadRequest();
            }

            _context.Entry(tarif).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarifExists(id))
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

        // POST: api/Tarifs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tarif>> PostTarif(Tarif tarif)
        {
            _context.Tarif.Add(tarif);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarif", new { id = tarif.IdT }, tarif);
        }

        // DELETE: api/Tarifs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tarif>> DeleteTarif(int id)
        {
            var tarif = await _context.Tarif.FindAsync(id);
            if (tarif == null)
            {
                return NotFound();
            }

            _context.Tarif.Remove(tarif);
            await _context.SaveChangesAsync();

            return tarif;
        }

        private bool TarifExists(int id)
        {
            return _context.Tarif.Any(e => e.IdT == id);
        }
    }
}
