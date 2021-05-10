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
    public class Rapport_ActiviteController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public Rapport_ActiviteController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Rapport_Activite
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rapport_Activite>>> GetRapport_Activite()
        {
            return await _context.Rapport_Activite.ToListAsync();
        }

        // GET: api/Rapport_Activite/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rapport_Activite>> GetRapport_Activite(int id)
        {
            var rapport_Activite = await _context.Rapport_Activite.FindAsync(id);

            if (rapport_Activite == null)
            {
                return NotFound();
            }

            return rapport_Activite;
        }

        // PUT: api/Rapport_Activite/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRapport_Activite(int id, Rapport_Activite rapport_Activite)
        {
            if (id != rapport_Activite.Id)
            {
                return BadRequest();
            }

            _context.Entry(rapport_Activite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Rapport_ActiviteExists(id))
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

        // POST: api/Rapport_Activite
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rapport_Activite>> PostRapport_Activite(Rapport_Activite rapport_Activite)
        {
            _context.Rapport_Activite.Add(rapport_Activite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRapport_Activite", new { id = rapport_Activite.Id }, rapport_Activite);
        }

        // DELETE: api/Rapport_Activite/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRapport_Activite(int id)
        {
            var rapport_Activite = await _context.Rapport_Activite.FindAsync(id);
            if (rapport_Activite == null)
            {
                return NotFound();
            }

            _context.Rapport_Activite.Remove(rapport_Activite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Rapport_ActiviteExists(int id)
        {
            return _context.Rapport_Activite.Any(e => e.Id == id);
        }
    }
}
