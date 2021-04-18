using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FestivalAPI.Data;
using FestivalAPI.Models;

using WebApplication1.ControllersAPI;

namespace WebApplication1.Controllers
{
        public class ArtistesController : Controller
        {
            /* private readonly APIContext _context;



             public ArtistesController(APIContext context)
             {
                 _context = context;
             }*/



            // GET: Artiste
            public IActionResult Index()
            {
                //return View(await _context.Artiste.Include("Links").ToListAsync());



                //IEnumerable<Artiste> Artistes = API.Instance.GetArtistesAsync().Result;
                //return View(Artistes);
                var Artistes = API.Instance.GetArtistesAsync().Result;








                return View(Artistes);
            }
        



        // GET: Artiste/Details/5
        public IActionResult Details(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }



                var Artiste = await _context.Artiste
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Artiste == null)
                {
                    return NotFound();
                }



                return View(Artiste);*/



                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetArtisteAsync(id).Result);
            }



            // GET: Artiste/Create
            public IActionResult Create()
            {
                return View();
            }



            // POST: Artiste/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create([Bind("IdA,Nom,Prenom,Photo,Style,Descriptif,LieuId,Pays,Extrait")] Artiste Artiste)
            {
                /*if (ModelState.IsValid)
                {
                    _context.Add(Artiste);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(Artiste);*/



                int drapeau = 0;
                IEnumerable<Artiste> Artistes = API.Instance.GetArtistesAsync().Result;
                foreach (var item in Artistes)
                {
                    if (item.Nom == Artiste.Nom)
                    {
                        drapeau++;
                    }
                }



                if (ModelState.IsValid && drapeau == 0)
                {
                    var URI = API.Instance.AjoutArtisteAsync(Artiste);
                    return RedirectToAction(nameof(Index));
                }
                else if (drapeau != 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(Artiste);
            }



            // GET: Artiste/Edit/5
            public IActionResult Edit(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }



                var Artiste = await _context.Artiste.FindAsync(id);
                if (Artiste == null)
                {
                    return NotFound();
                }
                return View(Artiste);*/



                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetArtisteAsync(id).Result);



            }



            // POST: Artiste/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(int id, [Bind("IdA,Nom,Prenom,Photo,Style,Descriptif,LieuId,Pays,Extrait")] Artiste Artiste)
            {
                /*if (id != Artiste.Id)
                {
                    return NotFound();
                }



                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Artiste);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ArtisteExists(Artiste.Id))
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
                return View(Artiste);*/
                if (id != Artiste.IdA)
                {
                    return NotFound();
                }





                if (ModelState.IsValid)
                {
                    var URI = API.Instance.ModifArtisteAsync(Artiste);
                    return RedirectToAction(nameof(Index));
                }
                return View(Artiste);
            }



            // GET: Artiste/Delete/5
            public IActionResult Delete(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }




                var Artiste = await _context.Artiste
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Artiste == null)
                {
                    return NotFound();
                }
                return View(Artiste);
                */
                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetArtisteAsync(id).Result);



            }



            // POST: Artiste/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                /*
                var Artiste = await _context.Artiste.FindAsync(id);
                _context.Artiste.Remove(Artiste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                */



                var URI = API.Instance.SupprArtisteAsync(id);
                return RedirectToAction(nameof(Index));



            }



            /*private bool ArtisteExists(int id)
            {
                return _context.Artiste.Any(e => e.Id == id);
            }*/
        }
    }
