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
    public class Rapport_GeosController : Controller
    {
        public IActionResult Rapport_Geo_Departement(int Id)
        {
            string Search = API.Instance.GetDepartementAsync(Id).Result.Nom;
            List<Rapport_Geo> rapport = (List<Rapport_Geo>)API.Instance.Rapport_Geo_DepartementAsync(Search).Result;

            return View(rapport);
        }

        public IActionResult Rapport_Geo_Region(int Id)
        {
            string Search = API.Instance.GetRegionAsync(Id).Result.Nom;
            List<Rapport_Geo> rapport = (List<Rapport_Geo>)API.Instance.Rapport_Geo_RegionAsync(Search).Result;

            return View(rapport);
        }

        public IActionResult Rapport_Geo_Pays(int Id)
        {
            string Search = API.Instance.GetPaysAsync(Id).Result.Nom;
            List<Rapport_Geo> rapport = (List<Rapport_Geo>)API.Instance.Rapport_Geo_PaysAsync(Search).Result;

            return View(rapport);
        }

        public IActionResult Rapport_Geo_Genre(int Id)
        {
            string Search = API.Instance.GetGenreAsync(Id).Result.Nom;
            List<Rapport_Geo> rapport = (List<Rapport_Geo>)API.Instance.Rapport_Geo_GenreAsync(Search).Result;

            return View(rapport);
        }

        public IActionResult Rapport_Geo_Departement_Graphe(int IdF, int IdD)
        {
            string Departement = API.Instance.GetDepartementAsync(IdD).Result.Nom;
           
            List<Rapport_Geo> rapport = (List<Rapport_Geo>)API.Instance.Rapport_Geo_DepartementAsync(Departement).Result;
            List<int> Nombre_Participants = new List<int>();
            List<string> Liste = new List<string>();

            foreach (var elmt in rapport)
            {
                int count = (int)API.Instance.Rapport_Geo_Count_DepartementAsync(IdF, Departement).Result;
                Nombre_Participants.Add(count);
                Liste.Add(elmt.Region);
            }

            string json1 = JsonConvert.SerializeObject(Liste);
            string json3 = JsonConvert.SerializeObject(Nombre_Participants);

            ViewBag.Liste = json1;
            ViewBag.Nombre_Participants = json3;

            ViewBag.Rapport_Geo = rapport;

            return View();
        }

        public IActionResult Rapport_Geo_Region_Graphe(int IdF, int IdR)
        {
            string Region = API.Instance.GetRegionAsync(IdR).Result.Nom;

            List<Rapport_Geo> rapport = (List<Rapport_Geo>)API.Instance.Rapport_Geo_RegionAsync(Region).Result;
            List<int> Nombre_Participants = new List<int>();
            List<string> Liste = new List<string>();

            foreach (var elmt in rapport)
            {
                int count = (int)API.Instance.Rapport_Geo_Count_RegionAsync(IdF, Region).Result;
                Nombre_Participants.Add(count);
                Liste.Add(elmt.Region);
            }

            string json1 = JsonConvert.SerializeObject(Liste);
            string json3 = JsonConvert.SerializeObject(Nombre_Participants);

            ViewBag.Liste = json1;
            ViewBag.Nombre_Participants = json3;

            ViewBag.Rapport_Geo = rapport;

            return View();
        }

        public IActionResult Rapport_Geo_Pays_Graphe(int IdF, int IdP)
        {
            string Pays = API.Instance.GetPaysAsync(IdP).Result.Nom;

            List<Rapport_Geo> rapport = (List<Rapport_Geo>)API.Instance.Rapport_Geo_PaysAsync(Pays).Result;
            List<int> Nombre_Participants = new List<int>();
            List<string> Liste = new List<string>();

            foreach (var elmt in rapport)
            {
                int count = (int)API.Instance.Rapport_Geo_Count_PaysAsync(IdF, Pays).Result;
                Nombre_Participants.Add(count);
                Liste.Add(elmt.Region);
            }

            string json1 = JsonConvert.SerializeObject(Liste);
            string json3 = JsonConvert.SerializeObject(Nombre_Participants);

            ViewBag.Liste = json1;
            ViewBag.Nombre_Participants = json3;

            ViewBag.Rapport_Geo = rapport;

            return View();
        }
    }
}
