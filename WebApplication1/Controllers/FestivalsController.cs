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
    public class FestivalsController : Controller
    {
        /* private readonly APIContext _context;

 

         public FestivalsController(APIContext context)
         {
             _context = context;
         }*/



        // GET: Festival
        public IActionResult Index()
        {
            //return View(await _context.Festival.Include("Links").ToListAsync());



            //IEnumerable<Festival> Festivals = API.Instance.GetFestivalsAsync().Result;
            //return View(Festivals);
            var Festivals = API.Instance.GetFestivalsAsync().Result;








            return View(Festivals);
        }




        // GET: Festival/Details/5
        public IActionResult Details(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

 

            var Festival = await _context.Festival
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Festival == null)
            {
                return NotFound();
            }

 

            return View(Festival);*/



            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetFestivalAsync(id).Result);
        }



        // GET: Festival/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Festival/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdF,Nom,Logo,Descriptif,Date_Debut,Date_Fin,LieuId")] Festival Festival)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(Festival);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Festival);*/



            int drapeau = 0;
            IEnumerable<Festival> Festivals = API.Instance.GetFestivalsAsync().Result;
            foreach (var item in Festivals)
            {
                if (item.Nom == Festival.Nom)
                {
                    drapeau++;
                }
            }



            if (ModelState.IsValid && drapeau == 0)
            {
                var URI = API.Instance.AjoutFestivalAsync(Festival);
                return RedirectToAction(nameof(Index));
            }
            else if (drapeau != 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(Festival);
        }



        // GET: Festival/Edit/5
        public IActionResult Edit(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

 

            var Festival = await _context.Festival.FindAsync(id);
            if (Festival == null)
            {
                return NotFound();
            }
            return View(Festival);*/



            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetFestivalAsync(id).Result);



        }



        // POST: Festival/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdF,Nom,Logo,Descriptif,Date_Debut,Date_Fin,LieuId")] Festival Festival)
        {
            /*if (id != Festival.Id)
            {
                return NotFound();
            }

 

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Festival);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FestivalExists(Festival.Id))
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
            return View(Festival);*/
            if (id != Festival.IdF)
            {
                return NotFound();
            }





            if (ModelState.IsValid)
            {
                var URI = API.Instance.ModifFestivalAsync(Festival);
                return RedirectToAction(nameof(Index));
            }
            return View(Festival);
        }



        // GET: Festival/Delete/5
        public IActionResult Delete(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

 


            var Festival = await _context.Festival
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Festival == null)
            {
                return NotFound();
            }
            return View(Festival);
            */
            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetFestivalAsync(id).Result);



        }



        // POST: Festival/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            /*
            var Festival = await _context.Festival.FindAsync(id);
            _context.Festival.Remove(Festival);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            */



            var URI = API.Instance.SupprFestivalAsync(id);
            return RedirectToAction(nameof(Index));



        }



        /*private bool FestivalExists(int id)
        {
            return _context.Festival.Any(e => e.Id == id);
        }*/
    }
}
