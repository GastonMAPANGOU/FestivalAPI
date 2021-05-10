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
    public class AmisController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public AmisController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Amis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ami>>> GetAmitiés()
        {
            return await _context.Ami.ToListAsync();
        }

        // GET: api/Amis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ami>> GetAmitié(int id)
        {
            var ami = await _context.Ami.FindAsync(id);

            if (ami == null)
            {
                return NotFound();
            }

            return ami;
        }

        [HttpGet("{AmiDemandeur}/{AmiReceveur}")]
        public async Task<ActionResult<Ami>> GetAmitié(int amiDemandeur, int amiReceveur)
        {
            var ami = await _context.Ami.FirstOrDefaultAsync(a => a.AmiDemandeur== amiDemandeur && a.AmiReceveur == amiReceveur);

            if (ami == null)
            {
                return NotFound();
            }
            return ami;
        }

        // PUT: api/Amis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmi(int id, Ami ami)
        {
            if (id != ami.Id)
            {
                return BadRequest();
            }

            _context.Entry(ami).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmiExists(id))
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

        // POST: api/Amis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ami>> PostAmi(Ami ami)
        {
            _context.Ami.Add(ami);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmitié", new { id = ami.Id }, ami);
        }

        // DELETE: api/Amis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmi(int id)
        {
            var ami = await _context.Ami.FindAsync(id);
            if (ami == null)
            {
                return NotFound();
            }

            _context.Ami.Remove(ami);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AmiExists(int id)
        {
            return _context.Ami.Any(e => e.Id == id);
        }
    }
}
