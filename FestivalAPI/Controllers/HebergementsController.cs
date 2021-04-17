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
    public class HebergementsController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public HebergementsController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Hebergements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hebergement>>> GetHebergement()
        {
            return await _context.Hebergement.ToListAsync();
        }

        // GET: api/Hebergements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hebergement>> GetHebergement(int id)
        {
            var hebergement = await _context.Hebergement.FindAsync(id);

            if (hebergement == null)
            {
                return NotFound();
            }

            return hebergement;
        }

        // PUT: api/Hebergements/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHebergement(int id, Hebergement hebergement)
        {
            if (id != hebergement.IdH)
            {
                return BadRequest();
            }

            _context.Entry(hebergement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HebergementExists(id))
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

        // POST: api/Hebergements
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Hebergement>> PostHebergement(Hebergement hebergement)
        {
            _context.Hebergement.Add(hebergement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHebergement", new { id = hebergement.IdH }, hebergement);
        }

        // DELETE: api/Hebergements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hebergement>> DeleteHebergement(int id)
        {
            var hebergement = await _context.Hebergement.FindAsync(id);
            if (hebergement == null)
            {
                return NotFound();
            }

            _context.Hebergement.Remove(hebergement);
            await _context.SaveChangesAsync();

            return hebergement;
        }

        private bool HebergementExists(int id)
        {
            return _context.Hebergement.Any(e => e.IdH == id);
        }
    }
}
