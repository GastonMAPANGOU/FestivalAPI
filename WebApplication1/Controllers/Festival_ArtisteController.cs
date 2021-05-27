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

namespace WebApplication1.Controllers
{
    public class Festival_ArtisteController : Controller
    {
        /* private readonly APIContext _context;

         public Festival_ArtistesController(APIContext context)
         {
             _context = context;
         }*/

        // GET: Festival_Artiste
        public IActionResult Index(string searchString)
        {
            //return View(await _context.Festival_Artiste.Include("Festival_Artistes").ToListAsync());

            IEnumerable<Festival_Artiste> Festival_Artistes = API.Instance.GetFestival_ArtistesAsync().Result;
            IEnumerable<Festival_Artiste> Festival_Artistes2 = new List<Festival_Artiste>();
            IEnumerable<Festival> festivals = API.Instance.GetFestivalsAsync().Result;


            if (!String.IsNullOrEmpty(searchString))
            {
                foreach (var item in festivals)
                {
                    if (item.Nom.Contains(searchString))
                    {
                        foreach (Festival_Artiste it in Festival_Artistes)
                        {
                            if (it.FestivalId == item.IdF)
                            {
                                Festival_Artistes2.Append(it);
                            }
                        }
                    }

                }
                return View(Festival_Artistes2);
            }


            return View(Festival_Artistes);
        }

        //DashBoard
        public IActionResult Dashboard()
        {
            List<Festival_Artiste> Festival_Artistes = (List<Festival_Artiste>)API.Instance.GetFestival_ArtistesAsync().Result;
            //ViewBag.liste_pays= countries ;
            return View(Festival_Artistes);
        }

        public ActionResult Programme(int? id)
        {
            if (HttpContext.Session.GetInt32("ido") == null)
            {
                return null;
            }
            Organisateur organisateur = API.Instance.GetOrganisateurAsync((int)HttpContext.Session.GetInt32("ido")).Result;
            Festival festival = API.Instance.GetFestivalAsync(organisateur.FestivalId).Result;
            if (festival == null)
            {
                return null;
            }
            return View(festival.Festival_Artistes);

        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("ido") == null)
            {
                return null;
            }
            Organisateur organisateur = API.Instance.GetOrganisateurAsync((int)HttpContext.Session.GetInt32("ido")).Result;
            Festival festival = API.Instance.GetFestivalAsync(organisateur.FestivalId).Result;
            if (festival == null)
            {
                return null;
            }
            return View(festival);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Festival_Artiste fa,TimeSpan HeureDebut, TimeSpan HeureFin)
        {
            if (HttpContext.Session.GetInt32("ido") == null)
            {
                return null;
            }
            Organisateur organisateur = API.Instance.GetOrganisateurAsync((int)HttpContext.Session.GetInt32("ido")).Result;
            Festival festival = API.Instance.GetFestivalAsync(organisateur.FestivalId).Result;
            if (festival == null)
            {
                return null;
            }
            Jour jour = API.Instance.GetJourAsync(fa.JourId).Result;
            fa.HeureDebut = jour.Date_jour.Date + HeureDebut;
            fa.HeureFin = jour.Date_jour.Date + HeureFin;


            IEnumerable<Festival_Artiste> fas = API.Instance.GetFestival_ArtistesAsync().Result.Where(f => f.SceneId == fa.SceneId);
            fas = API.Instance.GetFestival_ArtistesAsync().Result.Where(f => f.ArtisteId == fa.ArtisteId && f.JourId == fa.JourId);
            
            if (fas.Count()!=0)
            {
                ModelState.AddModelError("error", "Cet artiste a déjà été programmé pour ce jour");
                return Create();
            }
            
            if (fa.HeureDebut > fa.HeureFin)
            {
                ModelState.AddModelError("error", "Heure de début est plus grande que l'heure de fin !!!");
                return Create();
            }
            foreach (var item in fas)
            {
                if ((item.HeureDebut > fa.HeureDebut && item.HeureDebut < fa.HeureFin) || (item.HeureFin > fa.HeureDebut && item.HeureFin < fa.HeureFin))
                {
                    ModelState.AddModelError("error", "Ce créneau est déjà pris pour cette scène !!!");
                    return Create();
                }
            }

            fas = API.Instance.GetFestival_ArtistesAsync().Result.Where(f => f.ArtisteId == fa.ArtisteId);
            foreach (var item in fas)
            {
                if ((item.HeureDebut > fa.HeureDebut && item.HeureDebut < fa.HeureFin) || (item.HeureFin > fa.HeureDebut && item.HeureFin < fa.HeureFin))
                {
                    ModelState.AddModelError("error", "Ce créneau est déjà pris pour cet artiste !!!");
                    return Create();
                }
            }

            var URI = API.Instance.AjoutFestival_ArtisteAsync(fa);
                return RedirectToAction(nameof(Index));
            
            //ViewBag.liste_pays= countries ;
            
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetInt32("ido") == null)
            {
                return null;
            }
            Organisateur organisateur = API.Instance.GetOrganisateurAsync((int)HttpContext.Session.GetInt32("ido")).Result;
            Festival festival = API.Instance.GetFestivalAsync(organisateur.FestivalId).Result;
            if (festival == null)
            {
                return null;
            }
            return View(API.Instance.GetFestival_ArtisteAsync(id).Result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Festival_Artiste fa, TimeSpan HeureDebut, TimeSpan HeureFin)
        {
            if (HttpContext.Session.GetInt32("ido") == null)
            {
                return null;
            }
            Organisateur organisateur = API.Instance.GetOrganisateurAsync((int)HttpContext.Session.GetInt32("ido")).Result;
            Festival festival = API.Instance.GetFestivalAsync(organisateur.FestivalId).Result;
            if (festival == null)
            {
                return null;
            }
            Jour jour = API.Instance.GetJourAsync(fa.JourId).Result;
            fa.HeureDebut = jour.Date_jour.Date + HeureDebut;
            fa.HeureFin = jour.Date_jour.Date + HeureFin;


            IEnumerable<Festival_Artiste> fas = API.Instance.GetFestival_ArtistesAsync().Result.Where(f => f.SceneId == fa.SceneId);
            fas = API.Instance.GetFestival_ArtistesAsync().Result.Where(f => f.ArtisteId == fa.ArtisteId && f.JourId == fa.JourId);

            if (fas.Count() != 0)
            {
                ModelState.AddModelError("error", "Cet artiste a déjà été programmé pour ce jour");
                return Edit(fa.Id);
            }

            if (fa.HeureDebut > fa.HeureFin)
            {
                ModelState.AddModelError("error", "Heure de début est plus grande que l'heure de fin !!!");
                return Edit(fa.Id);
            }
            foreach (var item in fas)
            {
                if ((item.HeureDebut > fa.HeureDebut && item.HeureDebut < fa.HeureFin) || (item.HeureFin > fa.HeureDebut && item.HeureFin < fa.HeureFin))
                {
                    ModelState.AddModelError("error", "Ce créneau est déjà pris pour cette scène !!!");
                    return Edit(fa.Id);
                }
            }

            fas = API.Instance.GetFestival_ArtistesAsync().Result.Where(f => f.ArtisteId == fa.ArtisteId);
            foreach (var item in fas)
            {
                if ((item.HeureDebut > fa.HeureDebut && item.HeureDebut < fa.HeureFin) || (item.HeureFin > fa.HeureDebut && item.HeureFin < fa.HeureFin))
                {
                    ModelState.AddModelError("error", "Ce créneau est déjà pris pour cet artiste !!!");
                    return Edit(fa.Id);
                }
            }

            var URI = API.Instance.ModifFestival_ArtisteAsync(fa);
            return RedirectToAction(nameof(Index));

            //ViewBag.liste_pays= countries ;

        }


        // GET: Festival_Artiste/Details/5
        public IActionResult Details(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

            var Festival_Artiste = await _context.Festival_Artiste
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Festival_Artiste == null)
            {
                return NotFound();
            }

            return View(Festival_Artiste);*/

            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetFestival_ArtisteAsync(id).Result);
        }





    }
}
