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
    public class Festival_ArtisteController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public Festival_ArtisteController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Festival_Artiste
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Festival_Artiste>>> GetFestival_Artiste()
        {
            return await _context.Festival_Artiste.ToListAsync();
        }

        // GET: api/Festival_Artiste/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Festival_Artiste>> GetFestival_Artiste(int id)
        {
            var festival_Artiste = await _context.Festival_Artiste.FindAsync(id);

            if (festival_Artiste == null)
            {
                return NotFound();
            }

            return festival_Artiste;
        }

        // PUT: api/Festival_Artiste/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFestival_Artiste(int id, Festival_Artiste festival_Artiste)
        {
            if (id != festival_Artiste.Id)
            {
                return BadRequest();
            }

            _context.Entry(festival_Artiste).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Festival_ArtisteExists(id))
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

        // POST: api/Festival_Artiste
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Festival_Artiste>> PostFestival_Artiste(Festival_Artiste festival_Artiste)
        {
            _context.Festival_Artiste.Add(festival_Artiste);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFestival_Artiste", new { id = festival_Artiste.Id }, festival_Artiste);
        }

        // DELETE: api/Festival_Artiste/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFestival_Artiste(int id)
        {
            var festival_Artiste = await _context.Festival_Artiste.FindAsync(id);
            if (festival_Artiste == null)
            {
                return NotFound();
            }

            _context.Festival_Artiste.Remove(festival_Artiste);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Festival_ArtisteExists(int id)
        {
            return _context.Festival_Artiste.Any(e => e.Id == id);
        }
    }
}
