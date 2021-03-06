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
    public class FavorisController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public FavorisController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Favoris
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favoris>>> GetFavoris()
        {
            return await _context.Favoris.ToListAsync();
        }

        // GET: api/Favoris/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Favoris>> GetFavoris(int id)
        {
            var favoris = await _context.Favoris.FindAsync(id);

            if (favoris == null)
            {
                return NotFound();
            }

            return favoris;
        }
        [HttpGet("{ArtisteId}/{FestivalierId}")]
        public async Task<ActionResult<Favoris>> GetAmitié(int artisteId, int festivalierId)
        {
            var favoris = await _context.Favoris.FirstOrDefaultAsync(a => a.ArtisteId == artisteId && a.FestivalierId == festivalierId);

            if (favoris == null)
            {
                return NotFound();
            }
            return favoris;
        }

        // PUT: api/Favoris/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoris(int id, Favoris favoris)
        {
            if (id != favoris.Id)
            {
                return BadRequest();
            }

            _context.Entry(favoris).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavorisExists(id))
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

        // POST: api/Favoris
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Favoris>> PostFavoris(Favoris favoris)
        {
            _context.Favoris.Add(favoris);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavoris", new { id = favoris.Id }, favoris);
        }

        // DELETE: api/Favoris/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoris(int id)
        {
            var favoris = await _context.Favoris.FindAsync(id);
            if (favoris == null)
            {
                return NotFound();
            }

            _context.Favoris.Remove(favoris);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavorisExists(int id)
        {
            return _context.Favoris.Any(e => e.Id == id);
        }
    }
}
