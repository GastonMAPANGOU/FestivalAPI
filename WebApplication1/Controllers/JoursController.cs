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
    public class JoursController : Controller
    {
        /* private readonly APIContext _context;



         public JoursController(APIContext context)
         {
             _context = context;
         }*/



        // GET: Jour
        public IActionResult Index()
        {
            //return View(await _context.Jour.Include("Links").ToListAsync());



            //IEnumerable<Jour> Jours = API.Instance.GetJoursAsync().Result;
            //return View(Jours);
            var Jours = API.Instance.GetJoursAsync().Result;








            return View(Jours);
        }




        // GET: Jour/Details/5
        public IActionResult Details(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }



            var Jour = await _context.Jour
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Jour == null)
            {
                return NotFound();
            }



            return View(Jour);*/



            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetJourAsync(id).Result);
        }



        // GET: Jour/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Jour/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdJ,Numero_jour,Date_jour")] Jour Jour)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(Jour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Jour);*/



            int drapeau = 0;
            IEnumerable<Jour> Jours = API.Instance.GetJoursAsync().Result;
            foreach (var item in Jours)
            {
                if (item.Numero_jour == Jour.Numero_jour)
                {
                    drapeau++;
                }
            }



            if (ModelState.IsValid && drapeau == 0)
            {
                var URI = API.Instance.AjoutJourAsync(Jour);
                return RedirectToAction(nameof(Index));
            }
            else if (drapeau != 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(Jour);
        }



        // GET: Jour/Edit/5
        public IActionResult Edit(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }



            var Jour = await _context.Jour.FindAsync(id);
            if (Jour == null)
            {
                return NotFound();
            }
            return View(Jour);*/



            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetJourAsync(id).Result);



        }



        // POST: Jour/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdJ,Numero_jour,Date_jour")] Jour Jour)
        {
            /*if (id != Jour.Id)
            {
                return NotFound();
            }



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Jour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JourExists(Jour.Id))
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
            return View(Jour);*/
            if (id != Jour.IdJ)
            {
                return NotFound();
            }





            if (ModelState.IsValid)
            {
                var URI = API.Instance.ModifJourAsync(Jour);
                return RedirectToAction(nameof(Index));
            }
            return View(Jour);
        }



        // GET: Jour/Delete/5
        public IActionResult Delete(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }




            var Jour = await _context.Jour
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Jour == null)
            {
                return NotFound();
            }
            return View(Jour);
            */
            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetJourAsync(id).Result);



        }



        // POST: Jour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            /*
            var Jour = await _context.Jour.FindAsync(id);
            _context.Jour.Remove(Jour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            */



            var URI = API.Instance.SupprJourAsync(id);
            return RedirectToAction(nameof(Index));



        }



        /*private bool JourExists(int id)
        {
            return _context.Jour.Any(e => e.Id == id);
        }*/
    }
}
