using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FestivalAPI.Models;
using Newtonsoft.Json;

namespace WebApplication1.ControllersAPI
{
    public sealed class API
    {
        private static readonly HttpClient client = new HttpClient();

        private API()
        {
            client.BaseAddress = new Uri("http://localhost:53991/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        private static readonly object padlock = new object();
        private static API instance = null;

        public static API Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new API();
                    }
                    return instance;
                }
            }
        }

        public async Task<Gimi> GetGimi(int id)
        {
            Gimi gimi = null;
            HttpResponseMessage response = client.GetAsync("api/gimis/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                gimi = JsonConvert.DeserializeObject<Gimi>(resp);
            }
            return gimi;
        }



        public async Task<Gimi> GetGimi(string login, string pwd)
        {
            Gimi gimi = null;
            HttpResponseMessage response = client.GetAsync("api/gimis/" + login + "/" + pwd).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                gimi = JsonConvert.DeserializeObject<Gimi>(resp);
            }
            return gimi;
        }

        public async Task<Festivalier> GetFestivalier(string login, string pwd)
        {
            Festivalier festivalier = null;
            HttpResponseMessage response = client.GetAsync("api/festivaliers/" + login + "/" + pwd).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                festivalier = JsonConvert.DeserializeObject<Festivalier>(resp);
            }
            return festivalier;
        }
        public async Task<Organisateur> GetOrganisateur(string login, string pwd)
        {
            Organisateur organisateur = null;
            HttpResponseMessage response = client.GetAsync("api/organisateurs/" + login + "/" + pwd).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                organisateur = JsonConvert.DeserializeObject<Organisateur>(resp);
            }
            return organisateur;
        }
    ////////////////////////////////////////////////////////////LIEU//////////////////////////////////////
        public async Task<ICollection<Lieu>> GetLieuxAsync()
        {
            ICollection<Lieu> lieux = new List<Lieu>();
            HttpResponseMessage response = client.GetAsync("api/lieux").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                lieux = JsonConvert.DeserializeObject<List<Lieu>>(resp);
            }
            return lieux;
        }

        public async Task<Lieu> GetLieuAsync(int? id)
        {
            Lieu lieu = null;
            HttpResponseMessage response = client.GetAsync("api/lieux/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                lieu = JsonConvert.DeserializeObject<Lieu>(resp);
            }
            return lieu;
        }

        public async Task<Uri> AjoutLieuAsync(Lieu lieu)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/lieux", lieu);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifLieuAsync(Lieu lieu)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/lieux/" + lieu.IdL, lieu);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprLieuAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/lieux/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


     /////////////////////////////////////Festival/////////////////////////////////////////////////////
        public async Task<ICollection<Festival>> GetFestivalsAsync()
        {
            ICollection<Festival> festivals = new List<Festival>();
            HttpResponseMessage response = client.GetAsync("api/festivals").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                festivals = JsonConvert.DeserializeObject<List<Festival>>(resp);
            }
            return festivals;
        }

        public async Task<Festival> GetFestivalAsync(int? id)
        {
            Festival festival = null;
            HttpResponseMessage response = client.GetAsync("api/festivals/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                festival = JsonConvert.DeserializeObject<Festival>(resp);
            }
            return festival;
        }

        public async Task<Uri> AjoutFestivalAsync(Festival festival)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/festivals", festival);
                int n = AjoutJoursAsync(festival).Result;
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifFestivalAsync(Festival festival)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/festivals/" + festival.IdF, festival);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprFestivalAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/festivals/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

     ////////////////////////////////////////////Region/////////////////////////////////////////////////
        public async Task<ICollection<Region>> GetRegionsAsync()
        {
            ICollection<Region> regions = new List<Region>();
            HttpResponseMessage response = client.GetAsync("api/regions").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                regions = JsonConvert.DeserializeObject<List<Region>>(resp);
            }
            return regions;
        }

        public async Task<Region> GetRegionAsync(int? id)
        {
            Region region = null;
            HttpResponseMessage response = client.GetAsync("api/regions/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                region = JsonConvert.DeserializeObject<Region>(resp);
            }
            return region;
        }

        public async Task<Uri> AjoutRegionAsync(Region region)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/regions", region);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifRegionAsync(Region region)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/regions/" + region.Id, region);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprRegionAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/regions/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

    ////////////////////////////////////////Artiste//////////////////////////////////////
        public async Task<ICollection<Artiste>> GetArtistesAsync()
        {
            ICollection<Artiste> artistes = new List<Artiste>();
            HttpResponseMessage response = client.GetAsync("api/artistes").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                artistes = JsonConvert.DeserializeObject<List<Artiste>>(resp);
            }
            return artistes;
        }

        public async Task<Artiste> GetArtisteAsync(int? id)
        {
            Artiste artiste = null;
            HttpResponseMessage response = client.GetAsync("api/artistes/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                artiste = JsonConvert.DeserializeObject<Artiste>(resp);
            }
            return artiste;
        }

        public async Task<Uri> AjoutArtisteAsync(Artiste artiste)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/artistes", artiste);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifArtisteAsync(Artiste artiste)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/artistes/" + artiste.IdA, artiste);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprArtisteAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/artistes/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

     ///////////////////////////////////////////FestivalArtiste///////////////////////////////////////////////
        public async Task<ICollection<Festival_Artiste>> GetFestival_ArtistesAsync()
        {
            ICollection<Festival_Artiste> festival_Artistes = new List<Festival_Artiste>();
            HttpResponseMessage response = client.GetAsync("api/festival_Artiste").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                festival_Artistes = JsonConvert.DeserializeObject<List<Festival_Artiste>>(resp);
            }
            return festival_Artistes;
        }

        public async Task<Festival_Artiste> GetFestival_ArtisteAsync(int? id)
        {
            Festival_Artiste festival_Artiste = null;
            HttpResponseMessage response = client.GetAsync("api/festival_Artiste/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                festival_Artiste = JsonConvert.DeserializeObject<Festival_Artiste>(resp);
            }
            return festival_Artiste;
        }

        public async Task<Uri> AjoutFestival_ArtisteAsync(Festival_Artiste festival_Artiste)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/festival_Artiste", festival_Artiste);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifFestival_ArtisteAsync(Festival_Artiste festival_Artiste)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/festival_Artiste/" + festival_Artiste.Id, festival_Artiste);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprFestival_ArtisteAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/festival_Artiste/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        ///////////////////////////////////////////Hebergement///////////////////////////////////////////////
        public async Task<ICollection<Hebergement>> GetHebergementsAsync()
        {
            ICollection<Hebergement> hebergements = new List<Hebergement>();
            HttpResponseMessage response = client.GetAsync("api/hebergements").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                hebergements = JsonConvert.DeserializeObject<List<Hebergement>>(resp);
            }
            return hebergements;
        }

        public async Task<Hebergement> GetHebergementAsync(int? id)
        {
            Hebergement hebergement = null;
            HttpResponseMessage response = client.GetAsync("api/hebergements/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                hebergement = JsonConvert.DeserializeObject<Hebergement>(resp);
            }
            return hebergement;
        }

        public async Task<Uri> AjoutHebergementAsync(Hebergement hebergement)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/hebergements", hebergement);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifHebergementAsync(Hebergement hebergement)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/hebergements/" + hebergement.IdH, hebergement);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprHebergementAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/hebergements/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        ///////////////////////////////////////////Jour///////////////////////////////////////////////
        public async Task<ICollection<Jour>> GetJoursAsync()
        {
            ICollection<Jour> jours = new List<Jour>();
            HttpResponseMessage response = client.GetAsync("api/jours").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                jours = JsonConvert.DeserializeObject<List<Jour>>(resp);
            }
            return jours;
        }

        public async Task<Jour> GetJourAsync(int? id)
        {
            Jour jour = null;
            HttpResponseMessage response = client.GetAsync("api/jours/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                jour = JsonConvert.DeserializeObject<Jour>(resp);
            }
            return jour;
        }

        public async Task<Uri> AjoutJourAsync(Jour jour)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/jours", jour);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<int> AjoutJoursAsync(Festival festival)
        {
            DateTime date = festival.Date_Debut;
            Jour jour = new Jour();
            jour.FestivalId = festival.IdF;
            int i = 1;
            //Festival_Artiste festival_Artiste = new Festival_Artiste();

            jour.Date_jour = date;

            while (jour.Date_jour < festival.Date_Fin)
            {

                jour.Numero_jour = "Jour" + i;
                await AjoutJourAsync(jour);
                jour.Date_jour = jour.Date_jour.AddDays(1);
                i++;
            }
            return i;

        }

        public async Task<Uri> ModifJourAsync(Jour jour)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/jours/" + jour.IdJ, jour);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprJourAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/jours/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        ///////////////////////////////////////////Departement///////////////////////////////////////////////
        public async Task<ICollection<Departement>> GetDepartementsAsync()
        {
            ICollection<Departement> departements = new List<Departement>();
            HttpResponseMessage response = client.GetAsync("api/departements").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                departements = JsonConvert.DeserializeObject<List<Departement>>(resp);
            }
            return departements;
        }

        public async Task<Departement> GetDepartementAsync(int? id)
        {
            Departement departement = null;
            HttpResponseMessage response = client.GetAsync("api/departements/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                departement = JsonConvert.DeserializeObject<Departement>(resp);
            }
            return departement;
        }

        public async Task<Uri> AjoutDepartementAsync(Departement departement)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/departements", departement);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifDepartementAsync(Departement departement)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/departements/" + departement.Id, departement);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprDepartementAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/departements/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        ///////////////////////////////////////////Organisateur///////////////////////////////////////////////
        public async Task<ICollection<Organisateur>> GetOrganisateursAsync()
        {
            ICollection<Organisateur> organisateurs = new List<Organisateur>();
            HttpResponseMessage response = client.GetAsync("api/organisateurs").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                organisateurs = JsonConvert.DeserializeObject<List<Organisateur>>(resp);
            }
            return organisateurs;
        }

        public async Task<Organisateur> GetOrganisateurAsync(int? id)
        {
            Organisateur organisateur = null;
            HttpResponseMessage response = client.GetAsync("api/organisateurs/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                organisateur = JsonConvert.DeserializeObject<Organisateur>(resp);
            }
            return organisateur;
        }

        public async Task<Uri> AjoutOrganisateurAsync(Organisateur organisateur)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/organisateurs", organisateur);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifOrganisateurAsync(Organisateur organisateur)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/organisateurs/" + organisateur.IdO, organisateur);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprOrganisateurAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/organisateurs/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        ///////////////////////////////////////////Scene///////////////////////////////////////////////
        public async Task<ICollection<Scene>> GetScenesAsync()
        {
            ICollection<Scene> scenes = new List<Scene>();
            HttpResponseMessage response = client.GetAsync("api/scenes").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                scenes = JsonConvert.DeserializeObject<List<Scene>>(resp);
            }
            return scenes;
        }

        public async Task<Scene> GetSceneAsync(int? id)
        {
            Scene scene = null;
            HttpResponseMessage response = client.GetAsync("api/scenes/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                scene = JsonConvert.DeserializeObject<Scene>(resp);
            }
            return scene;
        }

        public async Task<Uri> AjoutSceneAsync(Scene scene)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/scenes", scene);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifSceneAsync(Scene scene)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/scenes/" + scene.IdS, scene);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprSceneAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/scenes/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        ///////////////////////////////////////////Tarif///////////////////////////////////////////////
        public async Task<ICollection<Tarif>> GetTarifsAsync()
        {
            ICollection<Tarif> tarifs = new List<Tarif>();
            HttpResponseMessage response = client.GetAsync("api/tarifs").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                tarifs = JsonConvert.DeserializeObject<List<Tarif>>(resp);
            }
            return tarifs;
        }

        public async Task<Tarif> GetTarifAsync(int? id)
        {
            Tarif tarif = null;
            HttpResponseMessage response = client.GetAsync("api/tarifs/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                tarif = JsonConvert.DeserializeObject<Tarif>(resp);
            }
            return tarif;
        }

        public async Task<Uri> AjoutTarifAsync(Tarif tarif)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/tarifs", tarif);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifTarifAsync(Tarif tarif)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/tarifs/" + tarif.IdT, tarif);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprTarifAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/tarifs/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        ///////////////////////////////////////////TypeHebergement///////////////////////////////////////////////
        public async Task<ICollection<Type_Hebergement>> GetType_HebergementsAsync()
        {
            ICollection<Type_Hebergement> type_Hebergements = new List<Type_Hebergement>();
            HttpResponseMessage response = client.GetAsync("api/type_Hebergement").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                type_Hebergements = JsonConvert.DeserializeObject<List<Type_Hebergement>>(resp);
            }
            return type_Hebergements;
        }

        public async Task<Type_Hebergement> GetType_HebergementAsync(int? id)
        {
            Type_Hebergement type_Hebergement = null;
            HttpResponseMessage response = client.GetAsync("api/type_Hebergements/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                type_Hebergement = JsonConvert.DeserializeObject<Type_Hebergement>(resp);
            }
            return type_Hebergement;
        }

        public async Task<Uri> AjoutType_HebergementAsync(Type_Hebergement type_Hebergement)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/type_Hebergement", type_Hebergement);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifType_HebergementAsync(Type_Hebergement type_Hebergement)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/type_Hebergement/" + type_Hebergement.IDTH, type_Hebergement);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprType_HebergementAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/type_Hebergement/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public async Task<ICollection<Pays>> GetPaysAsync()
        {
            ICollection<Pays> payss = new List<Pays>();
            HttpResponseMessage response = client.GetAsync("api/pays").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                payss = JsonConvert.DeserializeObject<List<Pays>>(resp);
            }
            return payss;
        }

        public async Task<Pays> GetPaysAsync(int? id)
        {
            Pays pays = null;
            HttpResponseMessage response = client.GetAsync("api/pays/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                pays = JsonConvert.DeserializeObject<Pays>(resp);
            }
            return pays;
        }

        public async Task<Uri> AjoutPaysAsync(Pays pays)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/pays", pays);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifPaysAsync(Pays pays)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/pays/" + pays.Id, pays);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprPaysAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/pays/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        //////////////////////////////////////////////////////Style//////////////////////////////////////////////////////////
        public async Task<ICollection<Style>> GetStylesAsync()
        {
            ICollection<Style> styles = new List<Style>();
            HttpResponseMessage response = client.GetAsync("api/styles").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                styles = JsonConvert.DeserializeObject<List<Style>>(resp);
            }
            return styles;
        }

        public async Task<Style> GetStyleAsync(int? id)
        {
            Style style = null;
            HttpResponseMessage response = client.GetAsync("api/styles/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                style = JsonConvert.DeserializeObject<Style>(resp);
            }
            return style;
        }

        public async Task<Uri> AjoutStyleAsync(Style style)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/styles", style);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifStyleAsync(Style style)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/styles/" + style.Id, style);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprStyleAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/styles/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        ///////////////////////////////////////////////////////////////Festivalier//////////////////////////////////////////////////////////////////////
        public async Task<ICollection<Festivalier>> GetFestivaliersAsync()
        {
            ICollection<Festivalier> festivaliers = new List<Festivalier>();
            HttpResponseMessage response = client.GetAsync("api/festivaliers").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                festivaliers = JsonConvert.DeserializeObject<List<Festivalier>>(resp);
            }
            return festivaliers;
        }

        public async Task<Festivalier> GetFestivalierAsync(int? id)
        {
            Festivalier festivalier = null;
            HttpResponseMessage response = client.GetAsync("api/festivaliers/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                festivalier = JsonConvert.DeserializeObject<Festivalier>(resp);
            }
            return festivalier;
        }

        public async Task<Uri> AjoutFestivalierAsync(Festivalier festivalier)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/festivaliers", festivalier);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifFestivalierAsync(Festivalier festivalier)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/festivaliers/" + festivalier.Id, festivalier);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprFestivalierAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/festivaliers/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        ////////////////////////////////////////////Ami///////////////////////////////////////////////////////////////
        public async Task<ICollection<Ami>> GetAmitiésAsync()
        {
            ICollection<Ami> amis = new List<Ami>();
            HttpResponseMessage response = client.GetAsync("api/amis").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                amis = JsonConvert.DeserializeObject<List<Ami>>(resp);
            }
            return amis;
        }

        public async Task<Ami> GetAmitiéAsync(int? id)
        {
            Ami ami = null;
            HttpResponseMessage response = client.GetAsync("api/amis/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                ami = JsonConvert.DeserializeObject<Ami>(resp);
            }
            return ami;
        }

        public async Task<Ami> GetAmitiéAsync(int idami1, int idami2)
        {
            Ami ami = null;
            HttpResponseMessage response = client.GetAsync("api/amis/" + idami1 + "/" + idami2).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                ami = JsonConvert.DeserializeObject<Ami>(resp);
            }
            else
            {
                response = client.GetAsync("api/amis/" + idami2 + "/" + idami1).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    ami = JsonConvert.DeserializeObject<Ami>(resp);
                }

            }
            return ami;
        }

        public async Task<Uri> AjoutAmiAsync(Ami ami)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/amis", ami);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifAmiAsync(Ami ami)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/amis/" + ami.Id, ami);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprAmiAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/amis/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        ////////////////////////////////////////////////////Favoris Artiste////////////////////////////////////////////////////////////////
        public async Task<ICollection<Favoris>> GetFavorisAsync()
        {
            ICollection<Favoris> favoris = new List<Favoris>();
            HttpResponseMessage response = client.GetAsync("api/favoris").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                favoris = JsonConvert.DeserializeObject<List<Favoris>>(resp);
            }
            return favoris;
        }

        public async Task<Favoris> GetFavorissAsync(int? id)
        {
            Favoris favoris = null;
            HttpResponseMessage response = client.GetAsync("api/favoris/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                favoris = JsonConvert.DeserializeObject<Favoris>(resp);
            }
            return favoris;
        }

        public async Task<Favoris> GetFavorisAsync(int artisteId, int festivalierId)
        {
            Favoris favoris = null;
            HttpResponseMessage response = client.GetAsync("api/Favoris/" + artisteId + "/" + festivalierId).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                favoris = JsonConvert.DeserializeObject<Favoris>(resp);
            }
            else
            {
                response = client.GetAsync("api/Favoris/" + artisteId + "/" + festivalierId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    favoris = JsonConvert.DeserializeObject<Favoris>(resp);
                }

            }
            return favoris;
        }
        public async Task<Uri> AjoutFavorisAsync(Favoris favoris)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/favoris", favoris);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public async Task<Uri> ModifFavorisAsync(Favoris favoris)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/favoris/" + favoris.Id, favoris);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public async Task<Uri> SupprFavorisAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/favoris/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        ///////////////////////////////////////////////////Artiste-Festival////////////////////////////////////////////////

        public async Task<ICollection<ClassAssociation>> GetAssociationAsync()
        {
            ICollection<ClassAssociation> classAssociations = new List<ClassAssociation>();
            HttpResponseMessage response = client.GetAsync("api/ClassAssociations").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                classAssociations = JsonConvert.DeserializeObject<List<ClassAssociation>>(resp);
            }
            return classAssociations;
        }





        public async Task<ICollection<Genre>> GetGenresAsync()
        {
            ICollection<Genre> genres = new List<Genre>();
            HttpResponseMessage response = client.GetAsync("api/genres").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                genres = JsonConvert.DeserializeObject<List<Genre>>(resp);
            }
            return genres;
        }

        public async Task<Genre> GetGenreAsync(int? id)
        {
            Genre genre = null;
            HttpResponseMessage response = client.GetAsync("api/genres/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                genre = JsonConvert.DeserializeObject<Genre>(resp);
            }
            return genre;
        }

        public async Task<Uri> AjoutGenreAsync(Genre genre)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/genres", genre);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> ModifGenreAsync(Genre genre)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/genres/" + genre.Id, genre);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprGenreAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/genres/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        ///////////////////////////////////////////Rapport/////////////////////////////////////////////
        public async Task<ICollection<Rapport_Activite>> GetRapport_ActivitésAsync()
        {
            ICollection<Rapport_Activite> amis = new List<Rapport_Activite>();
            HttpResponseMessage response = client.GetAsync("api/rapport_activite").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                amis = JsonConvert.DeserializeObject<List<Rapport_Activite>>(resp);
            }
            return amis;
        }

        public async Task<Rapport_Activite> GetRapport_ActivitéAsync(int? id)
        {
            Rapport_Activite ami = null;
            HttpResponseMessage response = client.GetAsync("api/rapport_activite/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                ami = JsonConvert.DeserializeObject<Rapport_Activite>(resp);
            }
            return ami;
        }

        public async Task<Uri> AjoutRapport_ActiviteAsync(Rapport_Activite ra)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/rapport_activite", ra);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprRapport_ActiviteAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/rapport_activite/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<List<Rapport_Activite>> AjoutRapportActivitesAsync(Festival festival)
        {
            List<Rapport_Activite> ras = new List<Rapport_Activite>();
            Rapport_Activite ra = new Rapport_Activite();
            ra.FestivalId = festival.IdF;
            List<int> annees = new List<int>();


            foreach (var festivalier in festival.Festivaliers)
            {
                if (!annees.Contains(festivalier.Date_Inscription.Year))
                    annees.Add(festivalier.Date_Inscription.Year);
            }

            foreach (var lieu in Instance.GetLieuxAsync().Result)
            {
                if (festival.LieuId == lieu.IdL)
                {
                    foreach (var departement in Instance.GetDepartementsAsync().Result)
                    {
                        if (departement.Id == lieu.DepartementId)
                        {
                            ra.Departement = departement.Nom;
                            foreach (var region in Instance.GetRegionsAsync().Result)
                            {
                                if (region.Id == departement.RegionId)
                                {
                                    ra.Region = region.Nom;
                                }
                            }
                        }
                    }
                }
            }

            foreach (var annee in annees)
            {
                ra.Somme_Vente = 0;
                ra.Annee = annee;
                foreach (var festivalier in festival.Festivaliers)
                {
                    if (festivalier.Date_Inscription.Year == annee)
                        ra.Somme_Vente = ra.Somme_Vente + festivalier.Somme;
                }
                await Instance.AjoutRapport_ActiviteAsync(ra);
                ras.Add(ra);
            }

            return ras;
        }

        public async Task<Uri> ModifRapport_ActiviteAsync(Rapport_Activite Rapport_Activite)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/Rapport_Activite/" + Rapport_Activite.Id, Rapport_Activite);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Rapport_Temps>> GetRapport_TempsAsync()
        {
            ICollection<Rapport_Temps> amis = new List<Rapport_Temps>();
            HttpResponseMessage response = client.GetAsync("api/rapport_activite").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                amis = JsonConvert.DeserializeObject<List<Rapport_Temps>>(resp);
            }
            return amis;
        }

        public async Task<Rapport_Temps> GetRapport_TempsAsync(int? id)
        {
            Rapport_Temps ami = null;
            HttpResponseMessage response = client.GetAsync("api/rapport_temps/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                ami = JsonConvert.DeserializeObject<Rapport_Temps>(resp);
            }
            return ami;
        }

        public async Task<Uri> AjoutRapport_TempsAsync(Rapport_Temps ra)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/rapport_temps", ra);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Uri> SupprRapport_TempsAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/rapport_temps/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<Rapport_Activite>> Rapport_Activites_RegionAsync(string? Region)
        {
            List<Rapport_Activite> List = new List<Rapport_Activite>();
            HttpResponseMessage response = client.GetAsync("api/Rapport_Activite/Rapport_Activite_Region/"+Region).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                List = JsonConvert.DeserializeObject<List<Rapport_Activite>>(resp);
            }
            return List;
        }
        public async Task<IEnumerable<Rapport_Activite>> Rapport_Activites_FestivalAsync(int? FestivalId)
        {
            List<Rapport_Activite> List = new List<Rapport_Activite>();
            HttpResponseMessage response = client.GetAsync("api/Rapport_Activite/Rapport_Activite_Festival/" + FestivalId).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                List = JsonConvert.DeserializeObject<List<Rapport_Activite>>(resp);
            }
            return List;
        }
        public async Task<IEnumerable<Rapport_Activite>> Rapport_Activites_DepartementAsync(string? Departement)
        {
            List<Rapport_Activite> List = new List<Rapport_Activite>();
            HttpResponseMessage response = client.GetAsync("api/Rapport_Activite/Rapport_Activite_Departement/" + Departement).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                List = JsonConvert.DeserializeObject<List<Rapport_Activite>>(resp);
            }
            return List;
        }
        public async Task<IEnumerable<Rapport_Geo>> Rapport_Geo_PaysAsync(string? Pays)
        {
            List<Rapport_Geo> List = new List<Rapport_Geo>();
            HttpResponseMessage response = client.GetAsync("api/Rapport_Geo/Rapport_Geo_Pays/" + Pays).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                List = JsonConvert.DeserializeObject<List<Rapport_Geo>>(resp);
            }
            return List;
        }

        public async Task<int> Rapport_Geo_Count_PaysAsync(int? FestivalId, string? Pays)
        {
            int count = 0;
            HttpResponseMessage response = client.GetAsync("api/Rapport_Geo/Rapport_Geo_Count_Pays/" + FestivalId +"/"+Pays).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                count = JsonConvert.DeserializeObject<int>(resp);
            }
            return count;
        }

        public async Task<int> Rapport_Geo_Count_RegionAsync(int? FestivalId, string? Region)
        {
            int count = 0;
            HttpResponseMessage response = client.GetAsync("api/Rapport_Geo/Rapport_Geo_Count_Region/" + FestivalId + "/" + Region).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                count = JsonConvert.DeserializeObject<int>(resp);
            }
            return count;
        }

        public async Task<int> Rapport_Geo_Count_DepartementAsync(int? FestivalId, string? Departement)
        {
            int count = 0;
            HttpResponseMessage response = client.GetAsync("api/Rapport_Geo/Rapport_Geo_Count_Departement/" + FestivalId + "/" + Departement).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                count = JsonConvert.DeserializeObject<int>(resp);
            }
            return count;
        }

        public async Task<IEnumerable<Rapport_Geo>> Rapport_Geo_DepartementAsync(string? Departement)
        {
            List<Rapport_Geo> List = new List<Rapport_Geo>();
            HttpResponseMessage response = client.GetAsync("api/Rapport_Geo/Rapport_Geo_Departement/" + Departement).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                List = JsonConvert.DeserializeObject<List<Rapport_Geo>>(resp);
            }
            return List;
        }
        public async Task<IEnumerable<Rapport_Geo>> Rapport_Geo_RegionAsync(string? Region)
        {
            List<Rapport_Geo> List = new List<Rapport_Geo>();
            HttpResponseMessage response = client.GetAsync("api/Rapport_Geo/Rapport_Geo_Region/" + Region).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                List = JsonConvert.DeserializeObject<List<Rapport_Geo>>(resp);
            }
            return List;
        }
        public async Task<IEnumerable<Rapport_Geo>> Rapport_Geo_GenreAsync(string? Genre)
        {
            List<Rapport_Geo> List = new List<Rapport_Geo>();
            HttpResponseMessage response = client.GetAsync("api/Rapport_Geo/Rapport_Geo_Genre/" + Genre).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                List = JsonConvert.DeserializeObject<List<Rapport_Geo>>(resp);
            }
            return List;
        }
        public async Task<IEnumerable<Rapport_Temps>> Rapport_Temps_JourAsync(int FestivalId, DateTime? dateInscription)
        {
            List<Rapport_Temps> List = new List<Rapport_Temps>();
            HttpResponseMessage response = client.GetAsync("api/Rapport_Temps/Rapport_Temps_Jours/" + FestivalId).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                List = JsonConvert.DeserializeObject<List<Rapport_Temps>>(resp);
            }
            return List;
        }

        public async Task<IEnumerable<Rapport_Temps>> Rapport_Temps_FestivalAsync(int FestivalId)
        {
            List<Rapport_Temps> List = new List<Rapport_Temps>();
            HttpResponseMessage response = client.GetAsync("api/Rapport_Temps/Rapport_Temps_Festival/" + FestivalId).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                List = JsonConvert.DeserializeObject<List<Rapport_Temps>>(resp);
            }
            return List;
        }

        public async Task<List<Rapport_Temps>> AjoutRapportTempsAsync(Festival festival)
        {
            List<Rapport_Temps> ras = new List<Rapport_Temps>();
            Rapport_Temps rt = new Rapport_Temps();
            rt.FestivalId = festival.IdF;
            List<DateTime> dates = new List<DateTime>();

            foreach (var festivalier in festival.Festivaliers)
            {
                if (!dates.Contains(festivalier.Date_Inscription))
                    dates.Add(festivalier.Date_Inscription);
            }



            foreach (var date in dates)
            {
                rt.Date_Inscription = date;
                rt.Nombre_Inscription = 0;
                foreach (var festivalier in festival.Festivaliers)
                {
                    if (festivalier.Date_Inscription == date)
                        rt.Nombre_Inscription++;
                }
                await Instance.AjoutRapport_TempsAsync(rt);
                ras.Add(rt);
            }

            return ras;
        }

        public async Task<Uri> ModifRapport_TempsAsync(Rapport_Temps Rapport_Temps)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/Rapport_Temps/" + Rapport_Temps.Id, Rapport_Temps);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Rapport_Geo>> GetRapport_GeosAsync()
        {
            ICollection<Rapport_Geo> amis = new List<Rapport_Geo>();
            HttpResponseMessage response = client.GetAsync("api/rapport_geo").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                amis = JsonConvert.DeserializeObject<List<Rapport_Geo>>(resp);
            }
            return amis;
        }

        public async Task<Rapport_Geo> GetRapport_GeoAsync(int? id)
        {
            Rapport_Geo ami = null;
            HttpResponseMessage response = client.GetAsync("api/rapport_geo/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                ami = JsonConvert.DeserializeObject<Rapport_Geo>(resp);
            }
            return ami;
        }

        public async Task<Uri> AjoutRapport_GeoAsync(Rapport_Geo ra)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/rapport_geo", ra);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }




        public async Task<Uri> SupprRapport_GeoAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/rapport_geo/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public async Task<List<Rapport_Geo>> AjoutRapportGeoAsync(Festivalier festivalier)
        {
            List<Rapport_Geo> ras = new List<Rapport_Geo>();
            Rapport_Geo ra = new Rapport_Geo();
            Festival festival = Instance.GetFestivalAsync(festivalier.Id).Result;

            ra.FestivalId = festival.IdF;
            ra.FestivalierId = festivalier.Id;
            foreach (var genre in Instance.GetGenresAsync().Result)
            {
                if (genre.Id == festivalier.GenreId)
                {
                    ra.Genre = genre.Nom;
                }
            }

            foreach (var lieu in Instance.GetLieuxAsync().Result)
            {
                if (festivalier.LieuId == lieu.IdL)
                {
                    foreach (var departement in Instance.GetDepartementsAsync().Result)
                    {
                        if (departement.Id == lieu.DepartementId)
                        {
                            ra.Departement = departement.Nom;
                            foreach (var region in Instance.GetRegionsAsync().Result)
                            {
                                if (region.Id == departement.RegionId)
                                {
                                    ra.Region = region.Nom;
                                    foreach (var pays in Instance.GetPaysAsync().Result)
                                    {
                                        if (pays.Id == region.PaysId)
                                        {
                                            ra.Pays = pays.Nom;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            await Instance.AjoutRapport_GeoAsync(ra);
            return ras;
        }

        public async Task<Uri> ModifRapport_GeoAsync(Rapport_Geo Rapport_Geo)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/Rapport_Geo/" + Rapport_Geo.Id, Rapport_Geo);

                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Rapport_Temps>> GetRapport_GeoAsync()
        {
            ICollection<Rapport_Temps> amis = new List<Rapport_Temps>();
            HttpResponseMessage response = client.GetAsync("api/rapport_geo").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                amis = JsonConvert.DeserializeObject<List<Rapport_Temps>>(resp);
            }
            return amis;
        }



        public async void MAJAsync()
        {
            foreach (var item in Instance.GetRapport_ActivitésAsync().Result)
            {
                await Instance.SupprRapport_ActiviteAsync(item.Id);
            }
            foreach (var item in Instance.GetRapport_TempsAsync().Result)
            {
                await Instance.SupprRapport_TempsAsync(item.Id);
            }
            foreach (var item in Instance.GetRapport_GeosAsync().Result)
            {
                await Instance.SupprRapport_GeoAsync(item.Id);
            }
            foreach (var item in Instance.GetFestivalsAsync().Result)
            {
                await Instance.AjoutRapportActivitesAsync(item);
                await Instance.AjoutRapportTempsAsync(item);
            }
            foreach (var item in Instance.GetFestivaliersAsync().Result)
            {
                await Instance.AjoutRapportGeoAsync(item);
            }
        }
    }
}
    


