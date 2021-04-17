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
        public class HebergementsController : Controller
        {
            /* private readonly APIContext _context;



             public HebergementsController(APIContext context)
             {
                 _context = context;
             }*/



            // GET: Hebergement
            public IActionResult Index()
            {
                //return View(await _context.Hebergement.Include("Links").ToListAsync());



                //IEnumerable<Hebergement> Hebergements = API.Instance.GetHebergementsAsync().Result;
                //return View(Hebergements);
                var Hebergements = API.Instance.GetHebergementsAsync().Result;








                return View(Hebergements);
            }




            // GET: Hebergement/Details/5
            public IActionResult Details(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }



                var Hebergement = await _context.Hebergement
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Hebergement == null)
                {
                    return NotFound();
                }



                return View(Hebergement);*/



                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetHebergementAsync(id).Result);
            }



            // GET: Hebergement/Create
            public IActionResult Create()
            {
                return View();
            }



            // POST: Hebergement/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create([Bind("IdH,Lien,LieuId,Type_HebergementIDTH")] Hebergement Hebergement)
            {
                /*if (ModelState.IsValid)
                {
                    _context.Add(Hebergement);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(Hebergement);*/



                int drapeau = 0;
                IEnumerable<Hebergement> Hebergements = API.Instance.GetHebergementsAsync().Result;
                foreach (var item in Hebergements)
                {
                    if (item.Lien == Hebergement.Lien)
                    {
                        drapeau++;
                    }
                }



                if (ModelState.IsValid && drapeau == 0)
                {
                    var URI = API.Instance.AjoutHebergementAsync(Hebergement);
                    return RedirectToAction(nameof(Index));
                }
                else if (drapeau != 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(Hebergement);
            }



            // GET: Hebergement/Edit/5
            public IActionResult Edit(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }



                var Hebergement = await _context.Hebergement.FindAsync(id);
                if (Hebergement == null)
                {
                    return NotFound();
                }
                return View(Hebergement);*/



                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetHebergementAsync(id).Result);



            }



            // POST: Hebergement/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(int id, [Bind("IdH,Lien,LieuId,Type_HebergementIDTH")] Hebergement Hebergement)
            {
                /*if (id != Hebergement.Id)
                {
                    return NotFound();
                }



                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Hebergement);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!HebergementExists(Hebergement.Id))
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
                return View(Hebergement);*/
                if (id != Hebergement.IdH)
                {
                    return NotFound();
                }





                if (ModelState.IsValid)
                {
                    var URI = API.Instance.ModifHebergementAsync(Hebergement);
                    return RedirectToAction(nameof(Index));
                }
                return View(Hebergement);
            }



            // GET: Hebergement/Delete/5
            public IActionResult Delete(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }




                var Hebergement = await _context.Hebergement
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Hebergement == null)
                {
                    return NotFound();
                }
                return View(Hebergement);
                */
                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetHebergementAsync(id).Result);



            }



            // POST: Hebergement/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                /*
                var Hebergement = await _context.Hebergement.FindAsync(id);
                _context.Hebergement.Remove(Hebergement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                */



                var URI = API.Instance.SupprHebergementAsync(id);
                return RedirectToAction(nameof(Index));



            }



            /*private bool HebergementExists(int id)
            {
                return _context.Hebergement.Any(e => e.Id == id);
            }*/
        }
    }
