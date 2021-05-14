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
            var festivals = API.Instance.GetFestivalsAsync().Result;

            return View(festivals);
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
            ICollection<Festival> festivals = new List<Festival>();
            ICollection<Festival_Artiste> lieu_festival_artistes = new List<Festival_Artiste>();
            ICollection<Festival_Artiste> artiste_festival_artistes = new List<Festival_Artiste>();
            ICollection<Festival_Artiste> style_festival_artistes = new List<Festival_Artiste>();
            var scenes = from m in API.Instance.GetScenesAsync().Result
                            select m;
           
            List<Festival_Artiste> final_festival_artistes = new List<Festival_Artiste>();
            //recherche par lieu
            if (!String.IsNullOrEmpty(lieu))
            {
                lieux = lieux.Where(s => s.Commune.Contains(lieu));
                foreach (var item in lieux)
                {
                    foreach (var it in item.Festivals)
                    {
                        festivals.Add(API.Instance.GetFestivalAsync(it.IdF).Result);
                    }
                }
                foreach (var item in festivals)
                {
                    foreach (var it in item.Festival_Artistes)
                    {
                        lieu_festival_artistes.Add(API.Instance.GetFestival_ArtisteAsync(it.Id).Result);
                    }
                }

                foreach (var item in lieu_festival_artistes)
                {
                    if (!final_festival_artistes.Contains(item))
                        final_festival_artistes.Add(item);
                }
            }
            festivals.Clear();
            //recherche par artiste
            if (!String.IsNullOrEmpty(artiste))
            {
                artistes = artistes.Where(s => s.Nom.Contains(artiste));
                
                foreach (var item in artistes)
                {
                    foreach (var it in item.Festival_Artistes)
                    {
                        artiste_festival_artistes.Add(API.Instance.GetFestival_ArtisteAsync(it.Id).Result);
                    }
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
                    foreach (var it in item.Artistes)
                    {
                        festivals.Add(API.Instance.GetFestivalAsync(it.IdA).Result);
                    }
                }
                foreach (var item in festivals)
                {
                    foreach (var it in item.Festival_Artistes)
                    {
                        style_festival_artistes.Add(API.Instance.GetFestival_ArtisteAsync(it.Id).Result);
                    }
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


            return View(scenes);
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
        public IActionResult Create([Bind("IdF,Nom,Logo,Descriptif,Date_Debut,Date_Fin,LieuId")] Festival festival, IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        
        {
            int taillemax = 2097152;

            List<String> extensionsvalides = new List<String>();
            List<String> strcut = new List<String>();
            extensionsvalides.Add(".jpg");
            extensionsvalides.Add(".jpeg");
            extensionsvalides.Add(".gif");
            extensionsvalides.Add(".png");
            extensionsvalides.Add(".jfif");
            string fileNamtre = file.FileName;

            string fileName = "img/festivals/" + festival.Nom;
            string extension = Path.GetExtension(file.FileName);
            string chemin = fileName + extension.ToLower();
            if (file.Length < taillemax && extensionsvalides.Contains(extension))
            {
                using (FileStream fileStream = System.IO.File.Create("wwwroot/" + chemin))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
                festival.Logo = chemin;



                int drapeau = 0;
                IEnumerable<Festival> Festivals = API.Instance.GetFestivalsAsync().Result;
                foreach (var item in Festivals)
                {
                    if (item.Nom == festival.Nom)
                    {
                        drapeau++;
                    }
                }

                if (ModelState.IsValid && drapeau == 0)
                {
                    var URI = API.Instance.AjoutFestivalAsync(festival);
                    return RedirectToAction(nameof(Index));
                }
                else if (drapeau != 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(festival);
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

        // GET: Festivalier/Create
        public IActionResult AjoutFestivalier(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetFestivalAsync(id).Result);
        }

        


        // POST: Festivalier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AjoutFestivalier(Festivalier festivalier, double somme, int festivalId)
        {

            festivalier.Somme = (festivalier.Nb_ParticipantsPT * somme + festivalier.Nb_ParticipantsDT * somme * 0.5)*festivalier.NbJours;
            festivalier.FestivalId = festivalId;
            festivalier.Date_Inscription = DateTime.Now;
            int drapeau = 0;
            Festival festival = API.Instance.GetFestivalAsync(festivalId).Result;
            IEnumerable<Festivalier> Festivaliers = API.Instance.GetFestivaliersAsync().Result;

            if (festival.NbPlacesDispo > 0)
            {
                festival.NbPlacesDispo--;
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
                    var URI2 = API.Instance.ModifFestivalAsync(festival);
                    return RedirectToAction(nameof(Index));
                }
                else if (drapeau != 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

            return View(festivalier);
        }

        
        public IActionResult Festivaliers(int? id)
        {
            int drapeau = 0;
            Ami ami = new Ami();
            Festivalier festivalier = API.Instance.GetFestivalierAsync((int)HttpContext.Session.GetInt32("idf")).Result;


            if (id == null)
            {
                return View(API.Instance.GetFestivaliersAsync().Result.Where(f=>f.Id!=festivalier.Id && f.FestivalId == festivalier.FestivalId));
            }
            else
            {
                ami.AmiDemandeur = (int)HttpContext.Session.GetInt32("idf");
                ami.AmiReceveur = (int)id;
                ami.Accepted = false;
                ami.Vue = false;
                IEnumerable<Ami> amitiés = API.Instance.GetAmitiésAsync().Result;
                foreach (var item in amitiés)
                {
                    if ((item.AmiDemandeur == ami.AmiDemandeur && item.AmiReceveur == ami.AmiReceveur) || (item.AmiDemandeur == ami.AmiReceveur && item.AmiDemandeur == ami.AmiReceveur))
                    {
                        drapeau++;
                        ami.Id = item.Id;

                        var URI = API.Instance.ModifAmiAsync(ami);
                        return View(API.Instance.GetFestivaliersAsync().Result.Where(f => f.Id != festivalier.Id && f.FestivalId == festivalier.FestivalId));
                    }
                }

                if ( drapeau == 0)
                {
                    var URI = API.Instance.AjoutAmiAsync(ami);

                    return View(API.Instance.GetFestivaliersAsync().Result.Where(f => f.Id != festivalier.Id && f.FestivalId == festivalier.FestivalId));
                }
            }
            return View(API.Instance.GetFestivaliersAsync().Result.Where(f => f.Id != festivalier.Id && f.FestivalId == festivalier.FestivalId));
        }

        public IActionResult Demandes(int? id)
        {
            
            Ami ami = new Ami();
            Festivalier festivalier = API.Instance.GetFestivalierAsync((int)HttpContext.Session.GetInt32("idf")).Result;
            IEnumerable<Ami> amitiés = API.Instance.GetAmitiésAsync().Result.Where(a => a.AmiReceveur == festivalier.Id && !a.Accepted);
            
            ICollection<Festivalier> festivaliers = new List<Festivalier>();
            Festivalier fvl = API.Instance.GetFestivalierAsync((int)HttpContext.Session.GetInt32("idf")).Result;
            foreach (var item in amitiés)
            {
                item.Vue = true;
                var URI = API.Instance.ModifAmiAsync(item);
            }
            amitiés = API.Instance.GetAmitiésAsync().Result.Where(a => a.AmiReceveur == festivalier.Id && !a.Accepted);
            foreach (var item in amitiés)
            {
                fvl = API.Instance.GetFestivalierAsync(item.AmiDemandeur).Result;
                festivaliers.Add(fvl);
            }
            if(id!=null)
            {
                Ami amitié = API.Instance.GetAmitiéAsync((int)id,festivalier.Id).Result;
                amitié.Accepted = true;
                var URI = API.Instance.ModifAmiAsync(amitié);
            }
            
            return View(festivaliers);
        }

        public ActionResult DeleteAmitié(int? id)
        {
            Festivalier festivalier = API.Instance.GetFestivalierAsync((int)HttpContext.Session.GetInt32("idf")).Result;

            if (id != null)
            {
                Ami amitié = API.Instance.GetAmitiéAsync((int)id, festivalier.Id).Result;
                var URI = API.Instance.SupprAmiAsync(amitié.Id);
            }
            return Redirect("/Festivals/Festivaliers");
        }
    }
}
