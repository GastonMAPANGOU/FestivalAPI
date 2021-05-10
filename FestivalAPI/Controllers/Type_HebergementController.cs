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
    public class Type_HebergementController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public Type_HebergementController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Type_Hebergement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Type_Hebergement>>> GetType_Hebergement()
        {
            return await _context.Type_Hebergement.Include("Hebergements").ToListAsync();
        }

        // GET: api/Type_Hebergement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Type_Hebergement>> GetType_Hebergement(int id)
        {
            var type_Hebergement = await _context.Type_Hebergement.FindAsync(id);

            if (type_Hebergement == null)
            {
                return NotFound();
            }

            return type_Hebergement;
        }

        // PUT: api/Type_Hebergement/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutType_Hebergement(int id, Type_Hebergement type_Hebergement)
        {
            if (id != type_Hebergement.IDTH)
            {
                return BadRequest();
            }

            _context.Entry(type_Hebergement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Type_HebergementExists(id))
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

        // POST: api/Type_Hebergement
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Type_Hebergement>> PostType_Hebergement(Type_Hebergement type_Hebergement)
        {
            _context.Type_Hebergement.Add(type_Hebergement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetType_Hebergement", new { id = type_Hebergement.IDTH }, type_Hebergement);
        }

        // DELETE: api/Type_Hebergement/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Type_Hebergement>> DeleteType_Hebergement(int id)
        {
            var type_Hebergement = await _context.Type_Hebergement.FindAsync(id);
            if (type_Hebergement == null)
            {
                return NotFound();
            }

            _context.Type_Hebergement.Remove(type_Hebergement);
            await _context.SaveChangesAsync();

            return type_Hebergement;
        }

        private bool Type_HebergementExists(int id)
        {
            return _context.Type_Hebergement.Any(e => e.IDTH == id);
        }
    }
}
