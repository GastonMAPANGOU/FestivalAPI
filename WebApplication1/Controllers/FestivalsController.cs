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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(String lieu, String artiste, String style)
        {

            var artistes = from m in API.Instance.GetArtistesAsync().Result
                           select m;
            var artistes2 = from m in API.Instance.GetArtistesAsync().Result
                            select m;

            var lieux = from m in API.Instance.GetLieuxAsync().Result
                        select m;
            var styles = from m in API.Instance.GetStylesAsync().Result
                         select m;
            var festivals = from m in API.Instance.GetFestivalsAsync().Result
                            select m;
            var scenes = from m in API.Instance.GetScenesAsync().Result
                            select m;
            var lieu_festival_artistes = from m in API.Instance.GetFestival_ArtistesAsync().Result
                                         select m;
            var artiste_festival_artistes = from m in API.Instance.GetFestival_ArtistesAsync().Result
                                            select m;
            var style_festival_artistes = from m in API.Instance.GetFestival_ArtistesAsync().Result
                                          select m;
            List<Festival_Artiste> final_festival_artistes = new List<Festival_Artiste>();
            //recherche par lieu
            if (!String.IsNullOrEmpty(lieu))
            {
                lieux = lieux.Where(s => s.Commune.Contains(lieu));
                foreach (var item in lieux)
                {
                    festivals = festivals.Where(s => s.LieuId == item.IdL);
                }
                foreach (var item in festivals)
                {
                    lieu_festival_artistes = lieu_festival_artistes.Where(s => s.FestivalId == item.IdF);
                }
                foreach (var item in lieu_festival_artistes)
                {
                    if (!final_festival_artistes.Contains(item))
                        final_festival_artistes.Add(item);
                }
            }
            //recherche par artiste
            if (!String.IsNullOrEmpty(artiste))
            {
                artistes = artistes.Where(s => s.Nom.Contains(artiste));
                foreach (var item in artistes)
                {
                    artiste_festival_artistes = artiste_festival_artistes.Where(s => s.ArtisteId == item.IdA);
                }
                foreach (var item in artiste_festival_artistes)
                {
                    if (!final_festival_artistes.Contains(item))
                        final_festival_artistes.Add(item);
                }
            }
            // recherche par style
            if (!String.IsNullOrEmpty(style))
            {
                styles = styles.Where(s => s.Nom.Contains(style));
                foreach (var item in styles)
                {
                    artistes2 = artistes2.Where(s => s.StyleId == item.Id);
                }
                foreach (var item in artistes2)
                {
                    style_festival_artistes = style_festival_artistes.Where(s => s.ArtisteId == item.IdA);
                }
                foreach (var item in style_festival_artistes)
                {
                    if (!final_festival_artistes.Contains(item))
                        final_festival_artistes.Add(item);
                }
            }
            scenes = from m in API.Instance.GetScenesAsync().Result
                        select m;
            if (final_festival_artistes.Count != 0)
                foreach (var item in final_festival_artistes)
                {
                    scenes = scenes.Where(s => s.IdS == item.SceneId);
                }


            return View(festivals);
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
