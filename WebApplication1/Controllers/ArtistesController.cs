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
        public class ArtistesController : Controller
        {
            /* private readonly APIContext _context;



             public ArtistesController(APIContext context)
             {
                 _context = context;
             }*/



            // GET: Artiste
            public IActionResult Index()
            {
                //return View(await _context.Artiste.Include("Links").ToListAsync());



                //IEnumerable<Artiste> Artistes = API.Instance.GetArtistesAsync().Result;
                //return View(Artistes);
                var Artistes = API.Instance.GetArtistesAsync().Result;
                return View(Artistes);
            }
        



        // GET: Artiste/Details/5
        public IActionResult Details(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }



                var Artiste = await _context.Artiste
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Artiste == null)
                {
                    return NotFound();
                }



                return View(Artiste);*/



                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetArtisteAsync(id).Result);
            }



            // GET: Artiste/Create
            public IActionResult Create()
            {
                return View();
            }



        // POST: Artiste/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdA,Nom,Prenom,Photo,StyleId,Descriptif,PaysId,Extrait")] Artiste artiste, IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
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

            string fileName = "img/artistes/" + artiste.Nom;
            string extension = Path.GetExtension(file.FileName);
            string chemin = fileName + extension.ToLower();
            if (file.Length < taillemax && extensionsvalides.Contains(extension))
            {
                using (FileStream fileStream = System.IO.File.Create("wwwroot/"+chemin))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
                artiste.Photo = chemin;



                int drapeau = 0;
                IEnumerable<Artiste> Artistes = API.Instance.GetArtistesAsync().Result;
                foreach (var item in Artistes)
                {
                    if (item.Nom == artiste.Nom)
                    {
                        drapeau++;
                    }
                }

                if (ModelState.IsValid && drapeau == 0)
                {
                    var URI = API.Instance.AjoutArtisteAsync(artiste);
                    return RedirectToAction(nameof(Index));
                }
                else if (drapeau != 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(artiste);
        }



            // GET: Artiste/Edit/5
            public IActionResult Edit(int? id)
            {
                


                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetArtisteAsync(id).Result);



            }



            // POST: Artiste/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
        public IActionResult Edit(Artiste artiste, IFormFile file, IFormFile file2, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            //taf de la photo
            int taillemax = 2097152;

            List<String> extensionsvalides = new List<String>();
            List<String> strcut = new List<String>();
            extensionsvalides.Add(".jpg");
            extensionsvalides.Add(".jpeg");
            extensionsvalides.Add(".gif");
            extensionsvalides.Add(".png");
            extensionsvalides.Add(".jfif");

            if (file != null)
            {
                string fileName = "img/artistes/photos/" + artiste.Nom;
                string extension = Path.GetExtension(file.FileName);
                string chemin = fileName + extension.ToLower();
                //taf de l'extrait musical

                if (file.Length < taillemax && extensionsvalides.Contains(extension))
                {
                    if (System.IO.File.Exists(chemin))
                    {
                        System.IO.File.Delete(chemin);
                    }
                    using (FileStream fileStream = System.IO.File.Create("wwwroot/" + chemin))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                    artiste.Photo = chemin;
                }
                else
                {
                    ModelState.AddModelError("error", "Extension du fichier non reconnu ou le fichier est trop lourd");
                    return View(artiste);
                }
                
            }

            if (file2 != null)
            {
                //taf de l'extrait musical
                List<String> extensionsvalides2 = new List<String>();
                List<String> strcut2 = new List<String>();
                extensionsvalides2.Add(".mp3");


                string fileName2 = "img/artistes/extraits/" + artiste.Nom;
                string extension2 = Path.GetExtension(file2.FileName);
                string chemin2 = fileName2 + extension2.ToLower();
                if (extensionsvalides2.Contains(extension2))
                {
                    if (System.IO.File.Exists(chemin2))
                    {
                        System.IO.File.Delete(chemin2);
                    }
                    using (FileStream fileStream = System.IO.File.Create("wwwroot/" + chemin2))
                    {
                        file2.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                    artiste.Extrait = chemin2;
                }
                else
                {
                    ModelState.AddModelError("error", "Extension du fichier non reconnu");
                    return View(artiste);
                }
            }
            var URI = API.Instance.ModifArtisteAsync(artiste);
            return RedirectToAction(nameof(Index));
            
        }



        // GET: Artiste/Delete/5
        public IActionResult Delete(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }




                var Artiste = await _context.Artiste
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Artiste == null)
                {
                    return NotFound();
                }
                return View(Artiste);
                */
                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetArtisteAsync(id).Result);



            }



            // POST: Artiste/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                /*
                var Artiste = await _context.Artiste.FindAsync(id);
                _context.Artiste.Remove(Artiste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                */



                var URI = API.Instance.SupprArtisteAsync(id);
                return RedirectToAction(nameof(Index));

            }

        public IActionResult Favoris(int? id)
        {
            int drapeau = 0;
            Favoris favoris = new Favoris();
            Festivalier festivalier = API.Instance.GetFestivalierAsync((int)HttpContext.Session.GetInt32("idf")).Result;
            var Artistes = API.Instance.GetArtistesAsync().Result;


            if (id == null)
            {
                return View(API.Instance.GetArtistesAsync().Result);
            }
            else
            {
                favoris.ArtisteId = (int)id;
                favoris.FestivalierId = (int)HttpContext.Session.GetInt32("idf");
                favoris.Like = true;
                IEnumerable<Favoris> fav = API.Instance.GetFavorisAsync().Result;
                foreach (var item in fav)
                {
                    if ((item.ArtisteId == favoris.ArtisteId && item.FestivalierId == favoris.FestivalierId))
                    {
                        favoris.Id = item.Id;
                        var URI = API.Instance.ModifFavorisAsync(favoris);
                        return View(API.Instance.GetArtistesAsync().Result);
                    }
                }

                if (ModelState.IsValid && drapeau == 0)
                {
                    var URI = API.Instance.AjoutFavorisAsync(favoris);
                    return View(API.Instance.GetArtistesAsync().Result);
                }
            }
            return View(API.Instance.GetArtistesAsync().Result.Where(f => f.IdA != festivalier.Id && f.FestivalId == festivalier.FestivalId));
        }

        public ActionResult DeleteFavoris(int id)
        {
            Festivalier festivalier = API.Instance.GetFestivalierAsync((int)HttpContext.Session.GetInt32("idf")).Result;

            if (id != null)
            {
                Favoris favoris = API.Instance.GetFavorisAsync((int)id, festivalier.Id).Result;
                var URI = API.Instance.SupprFavorisAsync(favoris.Id);
            }
            return Redirect("/Artistes/Favoris");
        }
        public ActionResult ListeFavoris()
        {
            List<Favoris> Favoris = (List<Favoris>)API.Instance.GetFavorisAsync().Result;
            return View(Favoris);
        }        
        public ActionResult Programme(int? id)
        {
            if(id == null)
            {
                return View(API.Instance.GetFestival_ArtistesAsync().Result);
            }
            else
            {
                return View(API.Instance.GetFestival_ArtistesAsync().Result.Where(f=>f.ArtisteId==id));
            }
            
        }


    }
}
