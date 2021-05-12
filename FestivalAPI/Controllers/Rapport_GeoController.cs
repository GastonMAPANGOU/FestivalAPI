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
    public class Rapport_GeoController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public Rapport_GeoController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Rapport_Geo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rapport_Geo>>> GetRapport_Geo()
        {
            return await _context.Rapport_Geo.ToListAsync();
        }

        // GET: api/Rapport_Geo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rapport_Geo>> GetRapport_Geo(int id)
        {
            var rapport_Geo = await _context.Rapport_Geo.FindAsync(id);

            if (rapport_Geo == null)
            {
                return NotFound();
            }

            return rapport_Geo;
        }

        // PUT: api/Rapport_Geo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRapport_Geo(int id, Rapport_Geo rapport_Geo)
        {
            if (id != rapport_Geo.Id)
            {
                return BadRequest();
            }

            _context.Entry(rapport_Geo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Rapport_GeoExists(id))
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

        // POST: api/Rapport_Geo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rapport_Geo>> PostRapport_Geo(Rapport_Geo rapport_Geo)
        {
            _context.Rapport_Geo.Add(rapport_Geo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRapport_Geo", new { id = rapport_Geo.Id }, rapport_Geo);
        }

        // DELETE: api/Rapport_Geo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRapport_Geo(int id)
        {
            var rapport_Geo = await _context.Rapport_Geo.FindAsync(id);
            if (rapport_Geo == null)
            {
                return NotFound();
            }

            _context.Rapport_Geo.Remove(rapport_Geo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("Rapport_Geo_Departement/{departement}")]
        public ActionResult<List<Rapport_Geo>> Rapport_Geo_Departement(string departement )
        {
            List<Rapport_Geo> rapport = new List<Rapport_Geo>();
            Rapport_GeoDAO rapport_GeoDAO = new Rapport_GeoDAO();
            rapport = rapport_GeoDAO.Rapport_Geo_Departement(departement);
            return rapport;
        }

        [HttpGet("Rapport_Geo_Region/{region}")]
        public ActionResult<List<Rapport_Geo>> Rapport_Geo_Region(string region)
        {
            List<Rapport_Geo> rapport = new List<Rapport_Geo>();
            Rapport_GeoDAO rapport_GeoDAO = new Rapport_GeoDAO();
            rapport = rapport_GeoDAO.Rapport_Geo_Region(region);
            return rapport;
        }

        [HttpGet("Rapport_Geo_Pays/{Pays}")]
        public ActionResult<List<Rapport_Geo>> Rapport_Geo_Pays(string pays)
        {
            List<Rapport_Geo> rapport = new List<Rapport_Geo>();
            Rapport_GeoDAO rapport_GeoDAO = new Rapport_GeoDAO();
            rapport = rapport_GeoDAO.Rapport_Geo_Pays(pays);
            return rapport;
        }

        [HttpGet("Rapport_Geo_Genre/{Genre}")]
        public ActionResult<List<Rapport_Geo>> Rapport_Geo_Genre(string Genre)
        {
            List<Rapport_Geo> rapport = new List<Rapport_Geo>();
            Rapport_GeoDAO rapport_GeoDAO = new Rapport_GeoDAO();
            rapport = rapport_GeoDAO.Rapport_Geo_Genre(Genre);
            return rapport;
        }

        private bool Rapport_GeoExists(int id)
        {
            return _context.Rapport_Geo.Any(e => e.Id == id);
        }
    }
}
