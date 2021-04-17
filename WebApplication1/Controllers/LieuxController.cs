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
    public class LieuxController : Controller
    {
        /* private readonly APIContext _context;

 

         public LieuxController(APIContext context)
         {
             _context = context;
         }*/



        // GET: Lieu
        public IActionResult Index()
        {
            //return View(await _context.Lieu.Include("Links").ToListAsync());



            //IEnumerable<Lieu> Lieux = API.Instance.GetLieuxAsync().Result;
            //return View(Lieux);
            var Lieux = API.Instance.GetLieuxAsync().Result;








            return View(Lieux);
        }




        // GET: Lieu/Details/5
        public IActionResult Details(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

 

            var Lieu = await _context.Lieu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Lieu == null)
            {
                return NotFound();
            }

 

            return View(Lieu);*/



            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetLieuAsync(id).Result);
        }



        // GET: Lieu/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Lieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdL,Commune, Adresse")] Lieu Lieu)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(Lieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Lieu);*/



            int drapeau = 0;
            IEnumerable<Lieu> Lieux = API.Instance.GetLieuxAsync().Result;
            foreach (var item in Lieux)
            {
                if (item.Commune == Lieu.Commune)
                {
                    drapeau++;
                }
            }



            if (ModelState.IsValid && drapeau == 0)
            {
                var URI = API.Instance.AjoutLieuAsync(Lieu);
                return RedirectToAction(nameof(Index));
            }
            else if (drapeau != 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(Lieu);
        }



        // GET: Lieu/Edit/5
        public IActionResult Edit(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

 

            var Lieu = await _context.Lieu.FindAsync(id);
            if (Lieu == null)
            {
                return NotFound();
            }
            return View(Lieu);*/



            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetLieuAsync(id).Result);



        }



        // POST: Lieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdL,Commune,Adresse")] Lieu Lieu)
        {
            /*if (id != Lieu.Id)
            {
                return NotFound();
            }

 

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Lieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LieuExists(Lieu.Id))
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
            return View(Lieu);*/
            if (id != Lieu.IdL)
            {
                return NotFound();
            }





            if (ModelState.IsValid)
            {
                var URI = API.Instance.ModifLieuAsync(Lieu);
                return RedirectToAction(nameof(Index));
            }
            return View(Lieu);
        }



        // GET: Lieu/Delete/5
        public IActionResult Delete(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

 


            var Lieu = await _context.Lieu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Lieu == null)
            {
                return NotFound();
            }
            return View(Lieu);
            */
            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetLieuAsync(id).Result);



        }



        // POST: Lieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            /*
            var Lieu = await _context.Lieu.FindAsync(id);
            _context.Lieu.Remove(Lieu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            */



            var URI = API.Instance.SupprLieuAsync(id);
            return RedirectToAction(nameof(Index));



        }



        /*private bool LieuExists(int id)
        {
            return _context.Lieu.Any(e => e.Id == id);
        }*/
    }
}
