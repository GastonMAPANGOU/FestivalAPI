using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FestivalAPI.Data;
using FestivalAPI.Models;

namespace FestivalAPI.Controllers
{
    public class FavorisController : Controller
    {
        private readonly FestivalAPIContext _context;

        public FavorisController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: Favoris
        public async Task<IActionResult> Index()
        {
            return View(await _context.Favoris.ToListAsync());
        }

        // GET: Favoris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoris = await _context.Favoris
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoris == null)
            {
                return NotFound();
            }

            return View(favoris);
        }

        // GET: Favoris/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Favoris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArtisteId,FestivalierId,like")] Favoris favoris)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favoris);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(favoris);
        }

        // GET: Favoris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoris = await _context.Favoris.FindAsync(id);
            if (favoris == null)
            {
                return NotFound();
            }
            return View(favoris);
        }

        // POST: Favoris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArtisteId,FestivalierId,like")] Favoris favoris)
        {
            if (id != favoris.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoris);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavorisExists(favoris.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(favoris);
        }

        // GET: Favoris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoris = await _context.Favoris
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoris == null)
            {
                return NotFound();
            }

            return View(favoris);
        }

        // POST: Favoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favoris = await _context.Favoris.FindAsync(id);
            _context.Favoris.Remove(favoris);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavorisExists(int id)
        {
            return _context.Favoris.Any(e => e.Id == id);
        }
    }
}
