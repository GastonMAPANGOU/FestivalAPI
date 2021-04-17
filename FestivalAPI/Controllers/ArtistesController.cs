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
    public class ArtistesController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public ArtistesController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Artistes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artiste>>> GetArtiste()
        {
            return await _context.Artiste.ToListAsync();
        }

        // GET: api/Artistes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artiste>> GetArtiste(int id)
        {
            var artiste = await _context.Artiste.FindAsync(id);

            if (artiste == null)
            {
                return NotFound();
            }

            return artiste;
        }

        // PUT: api/Artistes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtiste(int id, Artiste artiste)
        {
            if (id != artiste.IdA)
            {
                return BadRequest();
            }

            _context.Entry(artiste).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtisteExists(id))
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

        // POST: api/Artistes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Artiste>> PostArtiste(Artiste artiste)
        {
            _context.Artiste.Add(artiste);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtiste", new { id = artiste.IdA }, artiste);
        }

        // DELETE: api/Artistes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Artiste>> DeleteArtiste(int id)
        {
            var artiste = await _context.Artiste.FindAsync(id);
            if (artiste == null)
            {
                return NotFound();
            }

            _context.Artiste.Remove(artiste);
            await _context.SaveChangesAsync();

            return artiste;
        }

        private bool ArtisteExists(int id)
        {
            return _context.Artiste.Any(e => e.IdA == id);
        }
    }
}
