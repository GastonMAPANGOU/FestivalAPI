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
        private FestivalAPI.Data.SendMail sendMail;
        
        /* private readonly APIContext _context;

 

         public FestivalsController(APIContext context)
         {
             _context = context;
         }*/



        // GET: Festival
        /* public IActionResult Index()
         {

             var festivals = API.Instance.GetFestivalsAsync().Result;

             return View(festivals);
         }*/
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Index(string searchString)
        {
            IEnumerable<Artiste> artistes = API.Instance.GetArtistesAsync().Result;
            IEnumerable<Scene> scenes = API.Instance.GetScenesAsync().Result;
            IEnumerable<Festival> festival = API.Instance.GetFestivalsAsync().Result;
            IEnumerable<Lieu> communes = API.Instance.GetLieuxAsync().Result;
            List<Festival> festivals = new List<Festival>();



            if (!String.IsNullOrEmpty(searchString))
            {
                //var rech = artistes.Where(e => e.Nom.Contains(searchString)).Select(e => e.Artiste_Festival);
                var fes1 = festival.Where(e => e.Nom.Contains(searchString));
                var fes2 = communes.Where(e => e.Commune.Contains(searchString)).Select(e => e.Festivals);
                var fes3 = artistes.Where(e => e.Nom.Contains(searchString)).Select(e => e.Festival_Artistes);
                var fes4 = scenes.Where(e => e.Nom.Contains(searchString)).Select(e => e.Festival_Artistes);
                
                foreach (var nomfes in fes1)
                {
                    var NomFest = API.Instance.GetFestivalAsync(nomfes.IdF).Result;
                    festivals.Add(NomFest);
                }
                foreach (var communefes in fes2)
                {
                    foreach (var idcommune in communefes)
                    {
                        var commune = API.Instance.GetLieuAsync(idcommune.IdF).Result;
                        var Lieu = API.Instance.GetFestivalAsync(commune.IdL).Result;
                        festivals.Add(Lieu);
                    }
                }
                foreach (var artistefes in fes3)
                {
                    foreach (var artiste in artistefes)
                    {
                        var NomArtiste = API.Instance.GetFestivalAsync(artiste.FestivalId).Result;
                        festivals.Add(NomArtiste);
                    }
                }
                foreach (var scenefes in fes4)
                {
                    foreach (var scene in scenefes)
                    {
                        var NomScene = API.Instance.GetFestivalAsync(scene.FestivalId).Result;
                        festivals.Add(NomScene);
                    }
                }


                return View(festivals);
            }
            else
            {
                IEnumerable<Festival> festivalss = API.Instance.GetFestivalsAsync().Result;
                return View(festivalss);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Index(string lieu, string artiste, string style)
        {
            //return View(await _context.Region.Include("Links").ToListAsync());

            //IEnumerable<Region> regions = API.Instance.GetRegionsAsync().Result;
            //return View(regions);
            var lieux = from m in API.Instance.GetLieuxAsync().Result
                          select m;
            var artistes = from m in API.Instance.GetArtistesAsync().Result
                        select m;
            var styles = from m in API.Instance.GetArtistesAsync().Result
                           select m;
            ICollection<Festival_Artiste> festival_Artistes = new List<Festival_Artiste>();
            ICollection<Festival> festivals = new List<Festival>();
            ICollection<Artiste> artistes2 = new List<Artiste>();

            if (!String.IsNullOrEmpty(lieu))
            {
                lieux = lieux.Where(s => s.Commune.Contains(lieu));
                foreach (var unlieu in lieux)
                {
                    foreach (var scene in unlieu.Scenes)
                    {
                        Scene scene1 = API.Instance.GetSceneAsync(scene.FestivalId).Result;
                        foreach(var festivalartiste in scene1.Festival_Artistes)
                        {
                            if (!festival_Artistes.Contains(festivalartiste))
                                festival_Artistes.Add(festivalartiste);
                        }
                    }

                }
            }

            if (!String.IsNullOrEmpty(artiste))
            {
                artistes = artistes.Where(s => s.Nom.Contains(artiste));
                foreach (var art in artistes)
                {
                    Artiste artist = API.Instance.GetArtisteAsync(art.FestivalId).Result;
                    foreach (var festivalartiste in artist.Festival_Artistes)
                    {
                        if (!festival_Artistes.Contains(festivalartiste))
                            festival_Artistes.Add(festivalartiste);
                    }
                }
            }

            

            if (!String.IsNullOrEmpty(style))
            {
                styles = styles.Where(s => s.Nom.Contains(style));
                foreach (var sty in styles)
                {
                    artistes2.Add(API.Instance.GetArtisteAsync((int)sty.FestivalId).Result);
                    foreach (var art in artistes2)
                    {
                        Artiste artist = API.Instance.GetArtisteAsync(art.FestivalId).Result;
                        foreach (var festivalartiste in artist.Festival_Artistes)
                        {
                            if (!festival_Artistes.Contains(festivalartiste))
                                festival_Artistes.Add(festivalartiste);
                        }
                    }
                }
            }
            return View(festival_Artistes);
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
        public IActionResult Edit()
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



        // POST: Festival/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Festival festival, IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
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
            if(festival.IdF==0)
            {
                return null;
            }

            if (file != null)
            {
                string fileName = "img/logos/" + festival.Nom;
                string extension = Path.GetExtension(file.FileName);
                string chemin = fileName + extension.ToLower();
                //taf de l'extrait musical

                if (file.Length < taillemax && extensionsvalides.Contains(extension))
                {
                    if (chemin != null)
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
                    }



                    festival.Logo = chemin;
                }
                else
                {
                    ModelState.AddModelError("error", "Extension du fichier non reconnu ou le fichier est trop lourd");
                    return View(festival);
                }

            }
            else
            {
                Festival fvl = API.Instance.GetFestivalAsync(festival.IdF).Result;
                festival.Logo = fvl.Logo;
            }

            
            var URI = API.Instance.ModifFestivalAsync(festival);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult EditTarif()
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



        // POST: Festival/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTarif(Festival festival)
        {
            var URI = API.Instance.ModifFestivalAsync(festival);
            return RedirectToAction(nameof(Index));
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
        public IActionResult AjoutFestivalier(Festivalier festivalier)
        {
            DateTime today = DateTime.Now;
            var days = (today.Date - festivalier.Birthday.Date).TotalDays;
            Festival festival = API.Instance.GetFestivalAsync(festivalier.FestivalId).Result;
            festivalier.IsPublished = false;
            festival.NbPlacesDispo = festival.NbPlacesDispo - (festivalier.Nb_ParticipantsDT + festivalier.Nb_ParticipantsPT);

            if (days < 18 * 365)
            {
                ModelState.AddModelError("error", "Vous n'êtes pas majeur!!!");
                return AjoutFestivalier(festivalier.FestivalId);
            }
            if (festival.NbPlacesDispo< (festivalier.Nb_ParticipantsDT+ festivalier.Nb_ParticipantsPT))
            {
                ModelState.AddModelError("error", "Pas assez de  places disponibles ? veuillez en prendre moins!");
                return AjoutFestivalier(festivalier.FestivalId);
            }
            festivalier.Somme = (festivalier.Nb_ParticipantsPT * festival.Montant + festivalier.Nb_ParticipantsDT * festival.Montant * 0.5)*festivalier.NbJours;
            
            festivalier.Date_Inscription = DateTime.Now;
            int drapeau = 0;
            
            IEnumerable<Festivalier> Festivaliers = API.Instance.GetFestivaliersAsync().Result;

            if (festival.NbPlacesDispo > 0)
            {
                foreach (var item in Festivaliers)
                {
                    if (item.Nom == festivalier.Nom)
                    {
                        drapeau++;
                    }
                }

                if (ModelState.IsValid && drapeau == 0)
                {
                    sendMail = new FestivalAPI.Data.SendMail();
                    string mailSubject="Inscription au festival "+festival.Nom;
                    string content="Votre inscripion au festival "+festival.Nom+" a bien été prise en compte vous allez bientôt recevoir un mail de validation de paiement. <br> pour l'instant vous pouvez d'ores et déjà vous connecter sur notre site internet <br> <br> Cordialement <br> <br> A bientôt sur Festi'Normandie." ;
                    
                    sendMail.ActionSendMail(festivalier.Login, mailSubject, content);
                    
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

        public IActionResult AjoutScene()
        {
            if (HttpContext.Session.GetInt32("ido") == null)
            {
                return null;
            }
            Organisateur organisateur = API.Instance.GetOrganisateurAsync((int)HttpContext.Session.GetInt32("ido")).Result;
            Festival festival = API.Instance.GetFestivalAsync(organisateur.FestivalId).Result;
            if(festival==null)
            {
                return null;
            }
                     
            return View(festival);
        }


        // POST: Scene/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AjoutScene(Scene scene)
        {
            
            int drapeau = 0;

            IEnumerable<Scene> scenes = API.Instance.GetScenesAsync().Result;

            
            foreach (var item in scenes)
            {
                if (item.Nom == scene.Nom)
                {
                    drapeau++;
                }
            }

            if (ModelState.IsValid && drapeau == 0)
            {
                var URI = API.Instance.AjoutSceneAsync(scene);
                return RedirectToAction(nameof(Index));
            }
            else if (drapeau != 0)
            {
                return RedirectToAction(nameof(Index));
            }
            
            

            return View(scene);
        }

        public IActionResult EditScene(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetFestivalAsync(id).Result);
        }


        // POST: Scene/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditScene(Scene scene)
        {
            var URI = API.Instance.ModifSceneAsync(scene);
            return View(scene);
        }

        public IActionResult DeleteScene(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetFestivalAsync(id).Result);
        }


        // POST: Scene/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmedScene(int id)
        {
            var URI = API.Instance.SupprSceneAsync(id);
            return Redirect("/Festivals/Festivaliers");
        }

        public IActionResult AjoutHebergement()
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


        // POST: Hebergement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AjoutHebergement(Hebergement hebergement)
        {

            int drapeau = 0;

            IEnumerable<Hebergement> hebergements = API.Instance.GetHebergementsAsync().Result;


            foreach (var item in hebergements)
            {
                if (item.Nom == hebergement.Nom)
                {
                    drapeau++;
                }
            }

            if (ModelState.IsValid && drapeau == 0)
            {
                var URI = API.Instance.AjoutHebergementAsync(hebergement);
                return RedirectToAction(nameof(Index));
            }
            else if (drapeau != 0)
            {
                return RedirectToAction(nameof(Index));
            }



            return View(hebergement);
        }

        public IActionResult EditHebergement(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetFestivalAsync(id).Result);
        }


        // POST: Hebergement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditHebergement(Hebergement hebergement)
        {
            var URI = API.Instance.ModifHebergementAsync(hebergement);
            return View(hebergement);
        }

        public IActionResult DeleteHebergement(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetFestivalAsync(id).Result);
        }


        // POST: Hebergement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmedHebergement(int id)
        {
            var URI = API.Instance.SupprHebergementAsync(id);
            return Redirect("/Festivals/Festivaliers");
        }

        // GET: Artiste/Create
        public IActionResult AjoutArtiste()
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



        // POST: Artiste/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AjoutArtiste(Artiste artiste, IFormFile file, IFormFile file2, [FromServices] IHostingEnvironment hostingEnvironment)
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
                    return AjoutArtiste();
                }
            }
            else
            {
                artiste.Photo = "img/artistes/photos/defaut.png";
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
                    return AjoutArtiste();
                }
            }
            int drapeau = 0;
            IEnumerable<Artiste> Artistes = API.Instance.GetArtistesAsync().Result;
            foreach (var item in Artistes)
            {
                if (item.Nom == artiste.Nom)
                {
                    drapeau++;
                }
            }

            if (drapeau == 0)
            {
                var URI = API.Instance.AjoutArtisteAsync(artiste);
                return RedirectToAction(nameof(Index));
            }
            else if (drapeau != 0)
            {
                ModelState.AddModelError("error", "Cet artiste a déjà été ajouté");
                return AjoutArtiste();
            }

            return AjoutArtiste();
        }
        public IActionResult Festivaliers()
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

            return View(API.Instance.GetFestivaliersAsync().Result.Where(f => f.Id != festivalier.Id && f.FestivalId == festivalier.FestivalId));
        }
        
        public ActionResult AjoutAmi(int? id)
        {
            int drapeau = 0;
            Ami ami = new Ami();
            Festivalier festivalier = API.Instance.GetFestivalierAsync((int)HttpContext.Session.GetInt32("idf")).Result;


            if (id == null)
            {
                return Redirect("/Festivals/Festivaliers");
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
                        ami.AmiDemandeur = item.AmiReceveur;
                        ami.AmiReceveur = item.AmiReceveur;

                        var URI2 = API.Instance.ModifAmiAsync(ami);
                        return Redirect("/Festivals/Festivaliers");
                    }
                }
                  var URI = API.Instance.AjoutAmiAsync(ami);
                
            }
            return Redirect("/Festivals/Festivaliers");
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

        public ActionResult AccepterAmitié(int? id)
        {
            Festivalier festivalier = API.Instance.GetFestivalierAsync((int)HttpContext.Session.GetInt32("idf")).Result;

            if (id != null)
            {
                Ami amitié = API.Instance.GetAmitiéAsync((int)id, festivalier.Id).Result;
                amitié.Accepted = true;
                var URI = API.Instance.ModifAmiAsync(amitié);
            }
            return Redirect("/Festivals/Festivaliers");
        }

        public ActionResult ValiderPaiement(int? id)
        {
            if (id != null)
            {
                return Redirect("/Festivals/Festivaliers");
            }
            Festivalier festivalier = API.Instance.GetFestivalierAsync(id).Result;
            Festival festival = API.Instance.GetFestivalAsync(festivalier.Id).Result;
            festivalier.InscriptionAccepted = true;
            var uri= API.Instance.ModifFestivalierAsync(festivalier);
            sendMail = new FestivalAPI.Data.SendMail();
            string mailSubject = "Validation de paiement pour le festival" + festival.Nom;
            string content = "Votre paiement de ticket pour le festival " + festival.Nom + " a bien été validée. Ci dessous votre facture: <br> <br> Nom:"+ festivalier.Prenom+" "+festivalier.Nom + "< br> Nombre de places Demi-Tarif:" + festivalier.Nb_ParticipantsDT+ " <br>Nombre de places Plein-Tarif:" + festivalier.Nb_ParticipantsPT + " Nombre de jours:" + festivalier.NbJours + "<br>Tarif-Plein: " + festival.Montant + " < br > Demi-Tarif: " + (festival.Montant/2) + " < br > Total: "+festivalier.Somme+"< br >Cordialement <br> <br> A bientôt sur Festi'Normandie.";

            sendMail.ActionSendMail(festivalier.Login, mailSubject, content);

            return Redirect("/Festivals/Festivaliers");
        }

        public IActionResult Scenes()
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

            return View(festival.Scenes);
        }

        public IActionResult Inscriptions()
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

            return View(API.Instance.GetFestivaliersAsync().Result.Where(f =>f.FestivalId== festival.IdF));
        }

        public IActionResult Ventes()
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

            return View(API.Instance.GetFestivaliersAsync().Result.Where(f => f.FestivalId == festival.IdF && f.InscriptionAccepted));
        }

        public IActionResult Hebergements()
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

            return View(festival.Hebergements);
        }

        public IActionResult Artistes()
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

            return View(festival.Artistes);
        }

        public IActionResult OuvrirInscriptions()
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
            festival.IsReachable = true;
            var uri = API.Instance.ModifFestivalAsync(festival);

            return Redirect("Home/OrganisateursPage");
        }

        public IActionResult FermerInscriptions()
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
            festival.IsReachable = false;
            var uri = API.Instance.ModifFestivalAsync(festival);

            return Redirect("Home/OrganisateursPage");
        }

        public IActionResult Publier()
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
            festival.IsPublished = true;
            var uri = API.Instance.ModifFestivalAsync(festival);

            return Redirect("Home/OrganisateursPage");
        }

        public IActionResult Depublier()
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
            festival.IsPublished = false;
            var uri = API.Instance.ModifFestivalAsync(festival);

            return Redirect("Home/OrganisateursPage");
        }

        public IActionResult Retour()
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
            return Redirect("Home/OrganisateursPage");
        }

        public IActionResult Annuler()
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
            return View();
        }
        public IActionResult AnnulerConfirmed()
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
            festival.IsCanceled = true;
            var uri = API.Instance.ModifFestivalAsync(festival);
            sendMail = new FestivalAPI.Data.SendMail();
            foreach(var festivalier in festival.Festivaliers)
            {
                string mailSubject = "Festival Annulé:" + festival.Nom;
                string content = "Le festival " + festival.Nom + " a été annulé. Nous vos prions de contacter le +33 00 00 00 00 00 pour toute demande remboursement <br> <br> :< br >Cordialement <br> <br> A bientôt sur Festi'Normandie.";

                sendMail.ActionSendMail(festivalier.Login, mailSubject, content);
            }
            
            return Redirect("Index");
        }
        public ActionResult Programme(int? id)
        {
            if (id == null)
            {
                return View(API.Instance.GetFestival_ArtistesAsync().Result);
            }
            else
            {
                return View(API.Instance.GetFestival_ArtistesAsync().Result.Where(f => f.FestivalId == id));
            }

        }
    }
}
