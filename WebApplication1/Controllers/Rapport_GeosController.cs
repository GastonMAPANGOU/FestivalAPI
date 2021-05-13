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
        public IActionResult Rapport_Geo_Departement(string Search)
        {

            List<Rapport_Activite> rapport = (List<Rapport_Activite>)API.Instance.Rapport_Geo_DepartementAsync(Search).Result;

            return View(rapport);
        }

        public IActionResult Rapport_Geo_Region(string Search)
        {

            List<Rapport_Activite> rapport = (List<Rapport_Activite>)API.Instance.Rapport_Geo_RegionAsync(Search).Result;

            return View(rapport);
        }

        public IActionResult Rapport_Geo_Pays(string Search)
        {

            List<Rapport_Activite> rapport = (List<Rapport_Activite>)API.Instance.Rapport_Geo_PaysAsync(Search).Result;

            return View(rapport);
        }

        public IActionResult Rapport_Geo_Genre(string Search)
        {

            List<Rapport_Activite> rapport = (List<Rapport_Activite>)API.Instance.Rapport_Geo_GenreAsync(Search).Result;

            return View(rapport);
        }
    }
}
