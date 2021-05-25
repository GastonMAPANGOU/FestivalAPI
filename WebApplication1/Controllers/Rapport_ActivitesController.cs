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
    public class Rapport_ActivitesController : Controller
    {
        public IActionResult Rapport_Activite_Region(string region)
        {

            List<Rapport_Activite> rapport = (List<Rapport_Activite>)API.Instance.Rapport_Activites_RegionAsync(region).Result;
            
            return View(rapport);
        }

        public IActionResult Rapport_Activite_Departement(string Departement)
        {

            List<Rapport_Activite> rapport = (List<Rapport_Activite>)API.Instance.Rapport_Activites_DepartementAsync(Departement).Result;

            return View(rapport);
        }

        public IActionResult Rapport_Activite_Festival(int FestivalId)
        {

            List<Rapport_Activite> rapport = (List<Rapport_Activite>)API.Instance.Rapport_Activites_FestivalAsync(FestivalId).Result;

            return View(rapport);
        }
        public ActionResult Rapport_Activite_Region_Graphe(int Id)
        {
            string region = API.Instance.GetRegionAsync(Id).Result.Nom;

            List<Rapport_Activite> rapport = (List<Rapport_Activite>)API.Instance.Rapport_Activites_RegionAsync(region).Result;
            List<int> List_Year = new List<int>();
            List<double> List_Somme = new List<double>();
            foreach (var elmt in rapport)
            {
                List_Year.Add(elmt.Annee);
                List_Somme.Add(elmt.Somme_Vente);
            }

            string json1 = JsonConvert.SerializeObject(List_Year);
            string json3 = JsonConvert.SerializeObject(List_Somme);

            ViewBag.Liste_Year = json1;
            ViewBag.Liste_Somme = json3;

            ViewBag.Rapport_Activite = rapport;

            return View();
        }

        public IActionResult Rapport_Activite_Departement_Graphe(int Id)
        {
            string Departement = API.Instance.GetDepartementAsync(Id).Result.Nom;

            List<Rapport_Activite> rapport = (List<Rapport_Activite>)API.Instance.Rapport_Activites_DepartementAsync(Departement).Result;
            List<int> List_Year = new List<int>();
            List<double> List_Somme = new List<double>();
            foreach (var elmt in rapport)
            {
                List_Year.Add(elmt.Annee);
                List_Somme.Add(elmt.Somme_Vente);
            }

            string json1 = JsonConvert.SerializeObject(List_Year);
            string json3 = JsonConvert.SerializeObject(List_Somme);

            ViewBag.Liste_Year = json1;
            ViewBag.Liste_Somme = json3;

            ViewBag.Rapport_Activite = rapport;

            return View();
        }

        public IActionResult Rapport_Activite_Festival_Graphe(int Id)
        {

            List<Rapport_Activite> rapport = (List<Rapport_Activite>)API.Instance.Rapport_Activites_FestivalAsync(Id).Result;
            List<int> List_Year = new List<int>();
            List<double> List_Somme = new List<double>();
            foreach (var elmt in rapport)
            {
                List_Year.Add(elmt.Annee);
                List_Somme.Add(elmt.Somme_Vente);
            }

            string json1 = JsonConvert.SerializeObject(List_Year);
            string json3 = JsonConvert.SerializeObject(List_Somme);

            ViewBag.Liste_Year = json1;
            ViewBag.Liste_Somme = json3;

            ViewBag.Rapport_Activite = rapport;

            return View();
        }

        public IActionResult Index()
        {
            var Artistes = API.Instance.GetRegionsAsync().Result;

            return View(Artistes);
        }
        public IActionResult IndexFestival()
        {
            var Artistes = API.Instance.GetFestivalsAsync().Result;

            return View(Artistes);
        }
        public IActionResult IndexDepartement()
        {
            var Artistes = API.Instance.GetDepartementsAsync().Result;

            return View(Artistes);
        }
    }
}
