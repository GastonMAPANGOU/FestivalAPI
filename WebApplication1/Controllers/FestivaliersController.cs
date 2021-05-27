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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string searchString)
        {
            //return View(await _context.Festival.Include("Links").ToListAsync());



            //IEnumerable<Festival> Festivals = API.Instance.GetFestivalsAsync().Result;
            //return View(Festivals);


            //var Festivals = API.Instance.GetFestivalsAsync().Result;

            var festivals = from m in API.Instance.GetFestivalsAsync().Result
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                festivals = festivals.Where(s => s.Nom.Contains(searchString));
            }
            return View(festivals);
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
        public IActionResult Create(Festivalier festivalier, double somme,int festivalId)
        {
            
            festivalier.Somme = festivalier.Nb_ParticipantsPT*somme + festivalier.Nb_ParticipantsDT * somme*0.5;
            festivalier.FestivalId = festivalId;
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
        public IActionResult Edit(int id, [Bind("Id,Nom,Prenom,Login,Pwd,Nb_ParticipantsPT,Nb_ParticipantsDT,Somme,FestivalId")] Festivalier festivalier, double somme, int festivalId)
        {
            festivalier.Somme = festivalier.Nb_ParticipantsPT * somme + festivalier.Nb_ParticipantsDT * somme * 0.5;
            festivalier.FestivalId = festivalId;
            if (id != festivalier.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                var URI = API.Instance.ModifFestivalierAsync(festivalier);
                return RedirectToAction(nameof(Index));
            }
            return View(festivalier);
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
        public ActionResult Publier()
        {
            if (HttpContext.Session.GetInt32("idf") == null)
            {
                return null;
            }
            Festivalier festivalier = API.Instance.GetFestivalierAsync((int)HttpContext.Session.GetInt32("idf")).Result;
            Festival festival = API.Instance.GetFestivalAsync(festivalier.FestivalId).Result;
            if (festival == null)
            {
                return null;
            }
            festivalier.IsPublished = true;
            var uri = API.Instance.ModifFestivalierAsync(festivalier);

            return Redirect("/Home/Index");
        }

        public ActionResult Depublier()
        {
            if (HttpContext.Session.GetInt32("idf") == null)
            {
                return null;
            }
            Festivalier festivalier = API.Instance.GetFestivalierAsync((int)HttpContext.Session.GetInt32("idf")).Result;
            Festival festival = API.Instance.GetFestivalAsync(festivalier.FestivalId).Result;
            if (festival == null)
            {
                return null;
            }
            festivalier.IsPublished = false;
            var uri = API.Instance.ModifFestivalierAsync(festivalier);

            return Redirect("/Home/Index");
        }

    }
}
