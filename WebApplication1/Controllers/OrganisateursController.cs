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
        public class OrganisateursController : Controller
        {
            /* private readonly APIContext _context;



             public OrganisateursController(APIContext context)
             {
                 _context = context;
             }*/



            // GET: Organisateur
            public IActionResult Index()
            {
                //return View(await _context.Organisateur.Include("Links").ToListAsync());



                //IEnumerable<Organisateur> Organisateurs = API.Instance.GetOrganisateursAsync().Result;
                //return View(Organisateurs);
                var organisateurs = API.Instance.GetOrganisateursAsync().Result;








                return View(organisateurs);
            }




            // GET: Organisateur/Details/5
            public IActionResult Details(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }



                var Organisateur = await _context.Organisateur
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Organisateur == null)
                {
                    return NotFound();
                }



                return View(Organisateur);*/



                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetOrganisateurAsync(id).Result);
            }



            // GET: Organisateur/Create
            public IActionResult Create()
            {
                return View();
            }



            // POST: Organisateur/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create([Bind("IdO,Nom,Prenom,Login,Pwd,InscriptionAccepted,FestivalId")] Organisateur organisateur)
            {
                /*if (ModelState.IsValid)
                {
                    _context.Add(Organisateur);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(Organisateur);*/



                int drapeau = 0;
                IEnumerable<Organisateur> organisateurs = API.Instance.GetOrganisateursAsync().Result;
                foreach (var item in organisateurs)
                {
                    if (item.Nom == organisateur.Nom)
                    {
                        drapeau++;
                    }
                }



                if (ModelState.IsValid && drapeau == 0)
                {
                    var URI = API.Instance.AjoutOrganisateurAsync(organisateur);
                    return RedirectToAction(nameof(Index));
                }
                else if (drapeau != 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(organisateur);
            }



            // GET: Organisateur/Edit/5
            public IActionResult Edit(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }



                var Organisateur = await _context.Organisateur.FindAsync(id);
                if (Organisateur == null)
                {
                    return NotFound();
                }
                return View(Organisateur);*/



                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetOrganisateurAsync(id).Result);



            }



            // POST: Organisateur/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(int id, [Bind("IdO,Nom,Prenom,Login,Pwd,InscriptionAccepted,FestivalId")] Organisateur organisateur)
            {
                /*if (id != Organisateur.Id)
                {
                    return NotFound();
                }



                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Organisateur);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!OrganisateurExists(Organisateur.Id))
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
                return View(Organisateur);*/
                if (id != organisateur.IdO)
                {
                    return NotFound();
                }





                if (ModelState.IsValid)
                {
                    var URI = API.Instance.ModifOrganisateurAsync(organisateur);
                    return RedirectToAction(nameof(Index));
                }
                return View(organisateur);
            }



            // GET: Organisateur/Delete/5
            public IActionResult Delete(int? id)
            {
                /*if (id == null)
                {
                    return NotFound();
                }




                var Organisateur = await _context.Organisateur
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Organisateur == null)
                {
                    return NotFound();
                }
                return View(Organisateur);
                */
                if (id == null)
                {
                    return null;
                }
                return View(API.Instance.GetOrganisateurAsync(id).Result);



            }



            // POST: Organisateur/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                /*
                var Organisateur = await _context.Organisateur.FindAsync(id);
                _context.Organisateur.Remove(Organisateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                */



                var URI = API.Instance.SupprOrganisateurAsync(id);
                return RedirectToAction(nameof(Index));



            }



            /*private bool OrganisateurExists(int id)
            {
                return _context.Organisateur.Any(e => e.Id == id);
            }*/
        }
    }
