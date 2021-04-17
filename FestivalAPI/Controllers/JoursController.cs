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
    public class JoursController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public JoursController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Jours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jour>>> GetJour()
        {
            return await _context.Jour.Include("Festival_Artistes").Include("Tarifs").ToListAsync();
        }

        // GET: api/Jours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jour>> GetJour(int id)
        {
            var jour = await _context.Jour.FindAsync(id);

            if (jour == null)
            {
                return NotFound();
            }

            return jour;
        }

        // PUT: api/Jours/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJour(int id, Jour jour)
        {
            if (id != jour.IdJ)
            {
                return BadRequest();
            }

            _context.Entry(jour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JourExists(id))
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

        // POST: api/Jours
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Jour>> PostJour(Jour jour)
        {
            _context.Jour.Add(jour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJour", new { id = jour.IdJ }, jour);
        }

        // DELETE: api/Jours/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Jour>> DeleteJour(int id)
        {
            var jour = await _context.Jour.FindAsync(id);
            if (jour == null)
            {
                return NotFound();
            }

            _context.Jour.Remove(jour);
            await _context.SaveChangesAsync();

            return jour;
        }

        private bool JourExists(int id)
        {
            return _context.Jour.Any(e => e.IdJ == id);
        }
    }
}
