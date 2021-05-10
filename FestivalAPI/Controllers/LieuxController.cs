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
    public class LieuxController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public LieuxController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Lieux
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lieu>>> GetLieu()
        {
            return await _context.Lieu.Include("Hebergements").Include("Festivals").Include("Scenes").ToListAsync();
        }

        // GET: api/Lieux/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lieu>> GetLieu(int id)
        {
            var lieu = await _context.Lieu.FindAsync(id);

            if (lieu == null)
            {
                return NotFound();
            }

            return lieu;
        }

        // PUT: api/Lieux/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLieu(int id, Lieu lieu)
        {
            if (id != lieu.IdL)
            {
                return BadRequest();
            }

            _context.Entry(lieu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LieuExists(id))
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

        // POST: api/Lieux
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Lieu>> PostLieu(Lieu lieu)
        {
            _context.Lieu.Add(lieu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLieu", new { id = lieu.IdL }, lieu);
        }

        // DELETE: api/Lieux/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lieu>> DeleteLieu(int id)
        {
            var lieu = await _context.Lieu.FindAsync(id);
            if (lieu == null)
            {
                return NotFound();
            }

            _context.Lieu.Remove(lieu);
            await _context.SaveChangesAsync();

            return lieu;
        }

        private bool LieuExists(int id)
        {
            return _context.Lieu.Any(e => e.IdL == id);
        }
    }
}
