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
        public class StylesController : Controller
        {
            /* private readonly APIContext _context;



             public StylesController(APIContext context)
             {
                 _context = context;
             }*/



            // GET: Style
            public IActionResult Index()
            {
                //return View(await _context.Style.Include("Links").ToListAsync());



                //IEnumerable<Style> Styles = API.Instance.GetStylesAsync().Result;
                //return View(Styles);
                var styles = API.Instance.GetStylesAsync().Result;








                return View(styles);
            }




            // GET: Style/Details/5
            public IActionResult Details(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }



                var Style = await _context.Style
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Style == null)
                {
                    return NotFound();
                }



                return View(Style);*/



                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetStyleAsync(id).Result);
            }



            // GET: Style/Create
            public IActionResult Create()
            {
                return View();
            }



            // POST: Style/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create([Bind("IdS,Nom,Adresse,Capacite,Accessibilite,LieuId")] Style style)
            {
                int drapeau = 0;
                IEnumerable<Style> styles = API.Instance.GetStylesAsync().Result;
                foreach (var item in styles)
                {
                    if (item.Nom == style.Nom)
                    {
                        drapeau++;
                    }
                }



                if (ModelState.IsValid && drapeau == 0)
                {
                    var URI = API.Instance.AjoutStyleAsync(style);
                    return RedirectToAction(nameof(Index));
                }
                else if (drapeau != 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(style);
            }



            // GET: Style/Edit/5
            public IActionResult Edit(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }



                var Style = await _context.Style.FindAsync(id);
                if (Style == null)
                {
                    return NotFound();
                }
                return View(Style);*/



                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetStyleAsync(id).Result);



            }



            // POST: Style/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(int id, [Bind("IdS,Nom,Adresse,Capacite,Accessibilite,LieuId")] Style style)
            {
                /*if (id != Style.Id)
                {
                    return NotFound();
                }



                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Style);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StyleExists(Style.Id))
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
                return View(Style);*/
                if (id != style.Id)
                {
                    return NotFound();
                }





                if (ModelState.IsValid)
                {
                    var URI = API.Instance.ModifStyleAsync(style);
                    return RedirectToAction(nameof(Index));
                }
                return View(style);
            }



            // GET: Style/Delete/5
            public IActionResult Delete(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }




                var Style = await _context.Style
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Style == null)
                {
                    return NotFound();
                }
                return View(Style);
                */
                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetStyleAsync(id).Result);



            }



            // POST: Style/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                /*
                var Style = await _context.Style.FindAsync(id);
                _context.Style.Remove(Style);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                */



                var URI = API.Instance.SupprStyleAsync(id);
                return RedirectToAction(nameof(Index));



            }



            /*private bool StyleExists(int id)
            {
                return _context.Style.Any(e => e.Id == id);
            }*/
        }
    }
