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
    public class Type_HebergementsController : Controller
    {
        /* private readonly APIContext _context;

 

         public Type_HebergementsController(APIContext context)
         {
             _context = context;
         }*/



        // GET: Type_Hebergement
        public IActionResult Index()
        {
            //return View(await _context.Type_Hebergement.Include("Links").ToListAsync());



            //IEnumerable<Type_Hebergement> Type_Hebergements = API.Instance.GetType_HebergementsAsync().Result;
            //return View(Type_Hebergements);
            var Type_Hebergements = API.Instance.GetType_HebergementsAsync().Result;








            return View(Type_Hebergements);
        }




        // GET: Type_Hebergement/Details/5
        public IActionResult Details(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

 

            var Type_Hebergement = await _context.Type_Hebergement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Type_Hebergement == null)
            {
                return NotFound();
            }

 

            return View(Type_Hebergement);*/



            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetType_HebergementAsync(id).Result);
        }



        // GET: Type_Hebergement/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Type_Hebergement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDTH,Type")] Type_Hebergement Type_Hebergement)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(Type_Hebergement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Type_Hebergement);*/



            int drapeau = 0;
            IEnumerable<Type_Hebergement> Type_Hebergements = API.Instance.GetType_HebergementsAsync().Result;
            foreach (var item in Type_Hebergements)
            {
                if (item.Type == Type_Hebergement.Type)
                {
                    drapeau++;
                }
            }



            if (ModelState.IsValid && drapeau == 0)
            {
                var URI = API.Instance.AjoutType_HebergementAsync(Type_Hebergement);
                return RedirectToAction(nameof(Index));
            }
            else if (drapeau != 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(Type_Hebergement);
        }



        // GET: Type_Hebergement/Edit/5
        public IActionResult Edit(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

 

            var Type_Hebergement = await _context.Type_Hebergement.FindAsync(id);
            if (Type_Hebergement == null)
            {
                return NotFound();
            }
            return View(Type_Hebergement);*/



            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetType_HebergementAsync(id).Result);



        }



        // POST: Type_Hebergement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IDTH,Type")] Type_Hebergement Type_Hebergement)
        {
            /*if (id != Type_Hebergement.Id)
            {
                return NotFound();
            }

 

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Type_Hebergement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Type_HebergementExists(Type_Hebergement.Id))
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
            return View(Type_Hebergement);*/
            if (id != Type_Hebergement.IDTH)
            {
                return NotFound();
            }





            if (ModelState.IsValid)
            {
                var URI = API.Instance.ModifType_HebergementAsync(Type_Hebergement);
                return RedirectToAction(nameof(Index));
            }
            return View(Type_Hebergement);
        }



        // GET: Type_Hebergement/Delete/5
        public IActionResult Delete(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

 


            var Type_Hebergement = await _context.Type_Hebergement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Type_Hebergement == null)
            {
                return NotFound();
            }
            return View(Type_Hebergement);
            */
            if (id == null)
            {
                return null;
            }
            return View(API.Instance.GetType_HebergementAsync(id).Result);



        }



        // POST: Type_Hebergement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            /*
            var Type_Hebergement = await _context.Type_Hebergement.FindAsync(id);
            _context.Type_Hebergement.Remove(Type_Hebergement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            */



            var URI = API.Instance.SupprType_HebergementAsync(id);
            return RedirectToAction(nameof(Index));



        }



        /*private bool Type_HebergementExists(int id)
        {
            return _context.Type_Hebergement.Any(e => e.Id == id);
        }*/
    }
}
