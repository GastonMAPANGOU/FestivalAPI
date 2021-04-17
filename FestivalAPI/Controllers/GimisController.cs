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
    public class GimisController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public GimisController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Gimis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gimi>>> GetGimi()
        {
            return await _context.Gimi.ToListAsync();
        }

        // GET: api/Gimis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gimi>> GetGimi(int id)
        {
            var gimi = await _context.Gimi.FindAsync(id);

            if (gimi == null)
            {
                return NotFound();
            }

            return gimi;
        }

        // PUT: api/Gimis/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGimi(int id, Gimi gimi)
        {
            if (id != gimi.IdG)
            {
                return BadRequest();
            }

            _context.Entry(gimi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GimiExists(id))
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

        // POST: api/Gimis
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Gimi>> PostGimi(Gimi gimi)
        {
            _context.Gimi.Add(gimi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGimi", new { id = gimi.IdG }, gimi);
        }

        // DELETE: api/Gimis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gimi>> DeleteGimi(int id)
        {
            var gimi = await _context.Gimi.FindAsync(id);
            if (gimi == null)
            {
                return NotFound();
            }

            _context.Gimi.Remove(gimi);
            await _context.SaveChangesAsync();

            return gimi;
        }

        private bool GimiExists(int id)
        {
            return _context.Gimi.Any(e => e.IdG == id);
        }
    }
}
