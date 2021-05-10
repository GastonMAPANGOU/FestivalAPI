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
    public class Rapport_TempsController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public Rapport_TempsController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Rapport_Temps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rapport_Temps>>> GetRapport_Temps()
        {
            return await _context.Rapport_Temps.ToListAsync();
        }

        // GET: api/Rapport_Temps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rapport_Temps>> GetRapport_Temps(int id)
        {
            var rapport_Temps = await _context.Rapport_Temps.FindAsync(id);

            if (rapport_Temps == null)
            {
                return NotFound();
            }

            return rapport_Temps;
        }

        // PUT: api/Rapport_Temps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRapport_Temps(int id, Rapport_Temps rapport_Temps)
        {
            if (id != rapport_Temps.Id)
            {
                return BadRequest();
            }

            _context.Entry(rapport_Temps).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Rapport_TempsExists(id))
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

        // POST: api/Rapport_Temps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rapport_Temps>> PostRapport_Temps(Rapport_Temps rapport_Temps)
        {
            _context.Rapport_Temps.Add(rapport_Temps);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRapport_Temps", new { id = rapport_Temps.Id }, rapport_Temps);
        }

        // DELETE: api/Rapport_Temps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRapport_Temps(int id)
        {
            var rapport_Temps = await _context.Rapport_Temps.FindAsync(id);
            if (rapport_Temps == null)
            {
                return NotFound();
            }

            _context.Rapport_Temps.Remove(rapport_Temps);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Rapport_TempsExists(int id)
        {
            return _context.Rapport_Temps.Any(e => e.Id == id);
        }
    }
}
