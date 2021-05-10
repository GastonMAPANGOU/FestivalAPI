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
        public class ScenesController : Controller
        {
            /* private readonly APIContext _context;



             public ScenesController(APIContext context)
             {
                 _context = context;
             }*/



            // GET: Scene
            public IActionResult Index()
            {
                //return View(await _context.Scene.Include("Links").ToListAsync());



                //IEnumerable<Scene> Scenes = API.Instance.GetScenesAsync().Result;
                //return View(Scenes);
                var scenes = API.Instance.GetScenesAsync().Result;








                return View(scenes);
            }




            // GET: Scene/Details/5
            public IActionResult Details(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }



                var Scene = await _context.Scene
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Scene == null)
                {
                    return NotFound();
                }



                return View(Scene);*/



                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetSceneAsync(id).Result);
            }



            // GET: Scene/Create
            public IActionResult Create()
            {
                return View();
            }



            // POST: Scene/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create([Bind("IdS,Nom,Adresse,Capacite,Accessibilite,LieuId")] Scene scene)
            {
                int drapeau = 0;
                IEnumerable<Scene> scenes = API.Instance.GetScenesAsync().Result;
                foreach (var item in scenes)
                {
                    if (item.Nom == scene.Nom)
                    {
                        drapeau++;
                    }
                }



                if (ModelState.IsValid && drapeau == 0)
                {
                    var URI = API.Instance.AjoutSceneAsync(scene);
                    return RedirectToAction(nameof(Index));
                }
                else if (drapeau != 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(scene);
            }



            // GET: Scene/Edit/5
            public IActionResult Edit(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }



                var Scene = await _context.Scene.FindAsync(id);
                if (Scene == null)
                {
                    return NotFound();
                }
                return View(Scene);*/



                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetSceneAsync(id).Result);



            }



            // POST: Scene/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(int id, [Bind("IdS,Nom,Adresse,Capacite,Accessibilite,LieuId")] Scene scene)
            {
                /*if (id != Scene.Id)
                {
                    return NotFound();
                }



                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Scene);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SceneExists(Scene.Id))
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
                return View(Scene);*/
                if (id != scene.IdS)
                {
                    return NotFound();
                }





                if (ModelState.IsValid)
                {
                    var URI = API.Instance.ModifSceneAsync(scene);
                    return RedirectToAction(nameof(Index));
                }
                return View(scene);
            }



            // GET: Scene/Delete/5
            public IActionResult Delete(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }




                var Scene = await _context.Scene
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Scene == null)
                {
                    return NotFound();
                }
                return View(Scene);
                */
                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetSceneAsync(id).Result);



            }



            // POST: Scene/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                /*
                var Scene = await _context.Scene.FindAsync(id);
                _context.Scene.Remove(Scene);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                */



                var URI = API.Instance.SupprSceneAsync(id);
                return RedirectToAction(nameof(Index));



            }



            /*private bool SceneExists(int id)
            {
                return _context.Scene.Any(e => e.Id == id);
            }*/
        }
    }
