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
