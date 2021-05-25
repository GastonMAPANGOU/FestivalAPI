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
        public class AmisController : Controller
        {
            /* private readonly APIContext _context;



             public AmisController(APIContext context)
             {
                 _context = context;
             }*/



            // GET: Ami
            public IActionResult Index()
            {
                //return View(await _context.Ami.Include("Links").ToListAsync());



                //IEnumerable<Ami> Amis = API.Instance.GetAmisAsync().Result;
                //return View(Amis);
                var Amis = API.Instance.GetAmitiésAsync().Result;








                return View(Amis);
            }
        



        // GET: Ami/Details/5
        public IActionResult Details(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }



                var Ami = await _context.Ami
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Ami == null)
                {
                    return NotFound();
                }



                return View(Ami);*/



                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetAmitiéAsync(id).Result);
            }



            // GET: Ami/Create
            public IActionResult Create()
            {
                return View();
            }



        // POST: Ami/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Create(Ami ami)
        {
            int drapeau = 0;
            IEnumerable<Ami> amitiés = API.Instance.GetAmitiésAsync().Result;
            foreach (var item in amitiés)
            {
                if (item.AmiDemandeur == ami.AmiDemandeur && item.AmiReceveur == ami.AmiReceveur)
                {
                    drapeau++;
                }
            }

            if (ModelState.IsValid && drapeau == 0)
            {
                var URI = API.Instance.AjoutAmiAsync(ami);
                return Redirect("/Festivals/Festivaliers");
            }
            else if (drapeau != 0)
            {
                return Redirect("/Festivals/Festivaliers");
            }
             return Redirect("/Festivals/Festivaliers");
        }
        // POST: Ami/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(int? id)
        {
            int drapeau = 0;
            Ami ami = new Ami();
            ami.AmiDemandeur= (int)HttpContext.Session.GetInt32("idf");
            if (id==0)
            {
                return null;
            }ami.AmiReceveur = (int)id;
            ami.Accepted = false;
            ami.Vue= false;
            IEnumerable<Ami> amitiés = API.Instance.GetAmitiésAsync().Result;
            foreach (var item in amitiés)
            {
                if (item.AmiDemandeur == ami.AmiDemandeur && item.AmiReceveur == ami.AmiReceveur)
                {
                    ami=item;
                    var URI = API.Instance.ModifAmiAsync(ami);
                    return Redirect("/Festivals/Festivaliers");
                }
            }

            if (ModelState.IsValid && drapeau == 0)
            {
                var URI = API.Instance.AjoutAmiAsync(ami);
                return Redirect("/Festivals/Festivaliers");
            }
            else if (drapeau != 0)
            {
                return Redirect("/Festivals/Festivaliers");
            }
            return Redirect("/Festivals/Festivaliers");
        }

        // GET: Ami/Edit/5
        public IActionResult Edit(int? id)
            {

                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetAmitiéAsync(id).Result);
            }



            // POST: Ami/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
        public IActionResult Edit(Ami ami)
        {
            int drapeau = 0;
            IEnumerable<Ami> amitiés = API.Instance.GetAmitiésAsync().Result;
            foreach (var item in amitiés)
            {
                if (item.AmiDemandeur == ami.AmiDemandeur && item.AmiReceveur == ami.AmiReceveur)
                {
                    drapeau++;
                }
            }

            if (ModelState.IsValid && drapeau == 0)
            {
                var URI = API.Instance.ModifAmiAsync(ami);
                return RedirectToAction(nameof(Index));
            }
            else if (drapeau != 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(ami);
        }



        // GET: Ami/Delete/5
        public IActionResult Delete(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }




                var Ami = await _context.Ami
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Ami == null)
                {
                    return NotFound();
                }
                return View(Ami);
                */
                if (id == null)
                {
                    return null;
                }
                 return Redirect("/Festivals/Festivaliers");

        }



            // POST: Ami/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                var URI = API.Instance.SupprAmiAsync(id);
                 return Redirect("/Festivals/Festivaliers");
            }

       
        public IActionResult Partage(int id)
        {
             if (HttpContext.Session.GetInt32("idf") == null)
            {
                return null;
            }
            else
            {

                 int FestivalierId = (int)HttpContext.Session.GetInt32("idf");


                Ami ami = (Ami)API.Instance.GetAmitiéAsync(FestivalierId, id).Result;

                if (ami != null)
                {

                    IEnumerable<Favoris> favoris = API.Instance.GetFavorisAsync().Result.Where(s => s.FestivalierId == FestivalierId && s.Like == true);

                    List<Artiste> artistes = new List<Artiste>();

                    foreach (var item in favoris)
                    {
                        Artiste artiste = new Artiste();

                        artiste = (Artiste)API.Instance.GetArtisteAsync(item.ArtisteId).Result;

                        artistes.Add(artiste);
                    }


                    ViewBag.Artistes = artistes;
                }
                return View();
            }
           
        }

        public IActionResult Consult(int id)
        {
            if (HttpContext.Session.GetInt32("idf") == null)
            {
                return null;
            }
            else
            {

                int FestivalierId = (int)HttpContext.Session.GetInt32("idf");


                Ami ami = (Ami)API.Instance.GetAmitiéAsync(FestivalierId, id).Result;

                if (ami != null)
                {

                    IEnumerable<Favoris> favoris = API.Instance.GetFavorisAsync().Result.Where(s => s.FestivalierId == id && s.Like == true);

                    List<Artiste> artistes = new List<Artiste>();

                    foreach (var item in favoris)
                    {
                        Artiste artiste = new Artiste();

                        artiste = (Artiste)API.Instance.GetArtisteAsync(item.ArtisteId).Result;

                        artistes.Add(artiste);
                    }

                    ViewBag.Artistes = artistes;
                }
                return View();
            }
        }

        /*private bool AmiExists(int id)
        {
            return _context.Ami.Any(e => e.Id == id);
        }*/
    }
}
