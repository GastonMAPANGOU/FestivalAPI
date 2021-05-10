using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FestivalAPI.Data;
using WebApplication1.ControllersAPI;
using FestivalAPI.Models;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Controllers
{
    public class ConnexionController : Controller
    {
       


        // GET: Connexion
        public IActionResult ConnexionFestivalier()
        {
            Festivalier festivalier = new Festivalier();
            return View(festivalier);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConnexionFestivalier(Festivalier festivalier)
        {
            if (festivalier.Login != null && festivalier.Pwd != null)
            {
                festivalier = API.Instance.GetFestivalier(festivalier.Login, festivalier.Pwd).Result;
                if (festivalier != null)
                {
                    HttpContext.Session.SetInt32("idf", festivalier.Id);
                    HttpContext.Session.SetString("email", festivalier.Login);
                    HttpContext.Session.SetString("category", "Festivalier");
                    return Redirect("/Home/Index");
                }
                else
                {
                   ModelState.AddModelError("error", "Login et/ou mot de passe incorrect(s)");
                }
            }
            else
            {
                ModelState.AddModelError("error", "Login et/ou mot de passe incorrect(s)");
            }
            return View();
        }

        public IActionResult ConnexionOrganisateur()
        {
            Organisateur organisateur = new Organisateur();
            return View(organisateur);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConnexionOrganisateur(Organisateur organisateur)
        {
            if (organisateur.Login != null && organisateur.Pwd != null)
            {
                organisateur = API.Instance.GetOrganisateur(organisateur.Login, organisateur.Pwd).Result;
                if (organisateur != null)
                {
                    HttpContext.Session.SetInt32("ido", organisateur.IdO);
                    HttpContext.Session.SetString("email", organisateur.Login);
                    HttpContext.Session.SetString("category", "Organisateur");
                    return Redirect("/Festivals/Index");
                }
                else
                {
                    ModelState.AddModelError("error", "Login et/ou mot de passe incorrect(s)");
                }
            }
            else
            {
                ModelState.AddModelError("error", "Login et/ou mot de passe incorrect(s)");
            }
            return View();
        }

        public IActionResult Deconnexion()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home/Index");
        }
    }
}
