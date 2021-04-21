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
    public class FestivaliersController : Controller
    {
        /* private readonly APIContext _context;

 

         public FestivaliersController(APIContext context)
         {
             _context = context;
         }*/



        // GET: Festivalier
        public IActionResult Index()
        {
            //return View(await _context.Festivalier.Include("Links").ToListAsync());

            //IEnumerable<Festivalier> Festivaliers = API.Instance.GetFestivaliersAsync().Result;
            //return View(Festivaliers);
            var Festivaliers = API.Instance.GetFestivaliersAsync().Result;

            return View(Festivaliers);
        }

        


        // GET: Festivalier/Details/5
        public IActionResult Details(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

 

            var Festivalier = await _context.Festivalier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Festivalier == null)
            {
                return NotFound();
            }

 

            return View(Festivalier);*/



            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetFestivalierAsync(id).Result);
        }



        // GET: Festivalier/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Festivalier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nom,Prenom,Login,Pwd,Nb_Participants,Somme,FestivalId")] Festivalier festivalier,int id_jour, int coefficient, int id_festival=1)
        {
            
            IEnumerable<FestivalAPI.Models.Tarif> tarifs = WebApplication1.ControllersAPI.API.Instance.GetTarifsAsync().Result.Where(s => s.JourId == id_jour);
            foreach (var item in tarifs)
            {
                if (item.Coefficient == coefficient)
                {
                    festivalier.Somme = festivalier.Nb_Participants * coefficient *item.Montant;
                }
            }

            IEnumerable<FestivalAPI.Models.Jour> jours = WebApplication1.ControllersAPI.API.Instance.GetJoursAsync().Result.Where(s => s.IdJ == id_jour);
            foreach (var item in jours)
            {
                if (item.IdJ == id_jour)
                {
                    festivalier.FestivalId = (int)item.FestivalId;
                }
            }
          

            int drapeau = 0;
            IEnumerable<Festivalier> Festivaliers = API.Instance.GetFestivaliersAsync().Result;
            foreach (var item in Festivaliers)
            {
                if (item.Nom == festivalier.Nom)
                {
                    drapeau++;
                }
            }

            if (ModelState.IsValid && drapeau == 0)
            {
                var URI = API.Instance.AjoutFestivalierAsync(festivalier);
                return RedirectToAction(nameof(Index));
            }
            else if (drapeau != 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(festivalier);
        }



        // GET: Festivalier/Edit/5
        public IActionResult Edit(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

 

            var Festivalier = await _context.Festivalier.FindAsync(id);
            if (Festivalier == null)
            {
                return NotFound();
            }
            return View(Festivalier);*/



            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetFestivalierAsync(id).Result);



        }



        // POST: Festivalier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nom,Prenom,Login,Pwd,Nb_Participants,Somme,FestivalId")] Festivalier Festivalier)
        {
            /*if (id != Festivalier.Id)
            {
                return NotFound();
            }

 

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Festivalier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FestivalierExists(Festivalier.Id))
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
            return View(Festivalier);*/
            if (id != Festivalier.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                var URI = API.Instance.ModifFestivalierAsync(Festivalier);
                return RedirectToAction(nameof(Index));
            }
            return View(Festivalier);
        }



        // GET: Festivalier/Delete/5
        public IActionResult Delete(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

 


            var Festivalier = await _context.Festivalier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Festivalier == null)
            {
                return NotFound();
            }
            return View(Festivalier);
            */
            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetFestivalierAsync(id).Result);



        }



        // POST: Festivalier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            /*
            var Festivalier = await _context.Festivalier.FindAsync(id);
            _context.Festivalier.Remove(Festivalier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            */



            var URI = API.Instance.SupprFestivalierAsync(id);
            return RedirectToAction(nameof(Index));



        }



        /*private bool FestivalierExists(int id)
        {
            return _context.Festivalier.Any(e => e.Id == id);
        }*/
    }
}
