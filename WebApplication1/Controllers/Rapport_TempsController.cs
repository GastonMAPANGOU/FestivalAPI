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
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class Rapport_TempsController : Controller
    {
        public IActionResult Rapport_Temps_Jours(int Id, DateTime date)
        {

            List<Rapport_Temps> rapport = (List<Rapport_Temps>)API.Instance.Rapport_Temps_JourAsync(Id, date).Result;

            return View(rapport);
        }

        public IActionResult Rapport_Temps_Festival(int Id)
        {

            List<Rapport_Temps> rapport = (List<Rapport_Temps>)API.Instance.Rapport_Temps_FestivalAsync(Id).Result;

            return View(rapport);
        }

        public IActionResult Rapport_Temps_Jours_Graphe(DateTime date)
        {
            int Id = (int)HttpContext.Session.GetInt32("ido");
            int IdF = API.Instance.GetOrganisateurAsync(Id).Result.FestivalId;
            //List<Rapport_Temps> rapport = (List<Rapport_Temps>)API.Instance.Rapport_Temps_JourAsync(Id, date).Result;
            ICollection<Rapport_Temps> rapport = (ICollection<Rapport_Temps>)API.Instance.GetRapport_TempsAsync().Result.Where(s => s.FestivalId == IdF && s.Date_Inscription == date);
            List<DateTime> List_Date = new List<DateTime>();
            List<int> Nombre_Inscription = new List<int>();
            foreach (var elmt in rapport)
            {
                List_Date.Add(elmt.Date_Inscription);
                Nombre_Inscription.Add(elmt.Nombre_Inscription);
            }

            string json1 = JsonConvert.SerializeObject(List_Date);
            string json3 = JsonConvert.SerializeObject(Nombre_Inscription);


            ViewBag.Liste_Year = json1;
            ViewBag.Liste_Somme = json3;

            ViewBag.Rapport_Activite = rapport;

            return View();
        }

        public IActionResult Rapport_Temps_Festival_Graphe(int id)
        {
            int Ido = (int)HttpContext.Session.GetInt32("ido");
            int IdF = API.Instance.GetOrganisateurAsync(Ido).Result.FestivalId;
            List<Rapport_Temps> rapport = (List<Rapport_Temps>)API.Instance.Rapport_Temps_FestivalAsync(id).Result;
            List<DateTime> List_Date = new List<DateTime>();
            List<int> Nombre_Inscription = new List<int>();
            foreach (var elmt in rapport)
            {
                List_Date.Add(elmt.Date_Inscription);
                Nombre_Inscription.Add(elmt.Nombre_Inscription);
            }

            string json1 = JsonConvert.SerializeObject(List_Date);
            string json3 = JsonConvert.SerializeObject(Nombre_Inscription);


            ViewBag.Liste_Year = json1;
            ViewBag.Liste_Somme = json3;

            ViewBag.Rapport_Activite = rapport;

            return View();
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
