using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FestivalAPI.Data;
using FestivalAPI.Models;

namespace FestivalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenesController : ControllerBase
    {
        private readonly FestivalAPIContext _context;

        public ScenesController(FestivalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Scenes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scene>>> GetScene()
        {
            return await _context.Scene.Include("Festival_Artistes").ToListAsync();
        }

        // GET: api/Scenes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Scene>> GetScene(int id)
        {
            var scene = await _context.Scene.FindAsync(id);

            if (scene == null)
            {
                return NotFound();
            }

            return scene;
        }

        // PUT: api/Scenes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScene(int id, Scene scene)
        {
            if (id != scene.IdS)
            {
                return BadRequest();
            }

            _context.Entry(scene).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SceneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Scenes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Scene>> PostScene(Scene scene)
        {
            _context.Scene.Add(scene);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScene", new { id = scene.IdS }, scene);
        }

        // DELETE: api/Scenes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Scene>> DeleteScene(int id)
        {
            var scene = await _context.Scene.FindAsync(id);
            if (scene == null)
            {
                return NotFound();
            }

            _context.Scene.Remove(scene);
            await _context.SaveChangesAsync();

            return scene;
        }

        private bool SceneExists(int id)
        {
            return _context.Scene.Any(e => e.IdS == id);
        }

        [HttpGet("Filter_Scene_Artiste/{search}")]
        public ActionResult<List<Scene>> Filter_Scene_Artiste(string search)
        {
            StyleDAO styleDAO = new StyleDAO();
            ArtisteDAO artisteDAO = new ArtisteDAO();
            Festival_ArtisteDAO festival_ArtisteDAO = new Festival_ArtisteDAO();
            SceneDAO sceneDAO = new SceneDAO();

            List<Artiste> artistes = new List<Artiste>();
            List<int> list_IdScene = new List<int>();
            List<Scene> scenes = new List<Scene>();

            int Id = styleDAO.Return_IdStyle(search);
            artistes = artisteDAO.Return_Artiste_By_Style(Id);
            list_IdScene = festival_ArtisteDAO.List_Id_Scene(artistes);
            scenes = sceneDAO.List_Scene_Id(list_IdScene);

            return (scenes);
        }

        [HttpGet("Filter_Scene_Artiste_Style/{search}/{searchArtiste}")]
        public ActionResult<List<Scene>> Filter_Scene_Artiste_Style(string search, string searchArtiste)
        {
            StyleDAO styleDAO = new StyleDAO();
            ArtisteDAO artisteDAO = new ArtisteDAO();
            Festival_ArtisteDAO festival_ArtisteDAO = new Festival_ArtisteDAO();
            SceneDAO sceneDAO = new SceneDAO();
            List<Festival_Artiste> festival_Artistes = new List<Festival_Artiste>();
            List<Scene> scenes = new List<Scene>();

            int IdS = styleDAO.Return_IdStyle(search);
            int IdA = artisteDAO.Return_IdArtiste(searchArtiste);
            festival_Artistes = festival_ArtisteDAO.list_Festival_Artiste(IdS, IdA);
            scenes = sceneDAO.List_Scene(festival_Artistes);

            return (scenes);
        }

        [HttpGet("Filter_Scene_Lieu/{search}")]
        public ActionResult<List<Scene>> Filter_Scene_Lieu(string search)
        {
            LieuDAO lieuDAO = new LieuDAO();
            SceneDAO sceneDAO = new SceneDAO();
            List<Scene> scenes = new List<Scene>();
            int LieuId = lieuDAO.Return_IdLieu(search);

            scenes = sceneDAO.Display_By_Lieu(LieuId);

            return (scenes);
        }

        [HttpGet("Filter_Scene_Artiste_Style_Lieu/{search}/{searchArtiste}/{searchLieu}")]
        public ActionResult<List<Scene>> Filter_Scene_Artiste_Style_Lieu(string search, string searchArtiste, string searchLieu)
        {
            LieuDAO lieuDAO = new LieuDAO();
            StyleDAO styleDAO = new StyleDAO();
            ArtisteDAO artisteDAO = new ArtisteDAO();
            Festival_ArtisteDAO festival_ArtisteDAO = new Festival_ArtisteDAO();
            SceneDAO sceneDAO = new SceneDAO();
            List<Festival_Artiste> festival_Artistes = new List<Festival_Artiste>();
            List<Scene> scenes = new List<Scene>();

            int IdS = styleDAO.Return_IdStyle(search);
            int IdA = artisteDAO.Return_IdArtiste(searchArtiste);
            int LieuId = lieuDAO.Return_IdLieu(searchLieu);
            festival_Artistes = festival_ArtisteDAO.list_Festival_Artiste(IdS, IdA);
            scenes = sceneDAO.List_Scene_by_Artist_Lieu_Style(festival_Artistes, LieuId);

            return (scenes);
        }

        [HttpPut]
        public ActionResult<Scene> AddScene(int FestivalId, string Nom, string Adresse, int Capacite, bool Accessibilite, string lieu, DateTime dateJour, string artiste)
        {
            SceneDAO sceneDAO = new SceneDAO();
            Scene scene = new Scene();
            sceneDAO.InsertScene(FestivalId, Nom, Adresse, Capacite, Accessibilite, lieu, dateJour, artiste);
            scene = sceneDAO.GetScene_Nom(Nom);

            return scene;
        }


    }
}
