using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FestivalAPI.Models;
using Newtonsoft.Json;

namespace FestivalDeMusique.API
{
    public sealed class API
    {
        private static readonly HttpClient client = new HttpClient();

        private API()
        {
            client.BaseAddress = new Uri("http://localhost:53991");
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

        public async Task<ICollection<Gimi>> GetGimis()
        {
            ICollection<Gimi> listeGimis = new List<Gimi>();
            HttpResponseMessage response = client.GetAsync("api/gimis").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                listeGimis = JsonConvert.DeserializeObject<List<Gimi>>(resp);
            }
            return listeGimis;
        }

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

        public async Task<HttpResponseMessage> AjoutLieuAsync(Lieu lieu)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/lieux", lieu);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> ModifLieuAsync(Lieu lieu)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/lieux/" + lieu.IdL, lieu);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> SupprLieuAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/lieux/" + id);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

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

        public async Task<HttpResponseMessage> AjoutFestivalAsync(Festival festival)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/festivals", festival);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> ModifFestivalAsync(Festival festival)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/festivals/" + festival.IdF, festival);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
       
        
        
        public async Task<HttpResponseMessage> SupprFestivalAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/festivals/" + id);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

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

        public async Task<HttpResponseMessage> AjoutArtisteAsync(Artiste artiste)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/artistes", artiste);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> ModifArtisteAsync(Artiste artiste)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/artistes/" + artiste.IdA, artiste);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> SupprArtisteAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/artistes/" + id);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


        public async Task<ICollection<Festival_Artiste>> GetFestival_ArtistesAsync()
        {
            ICollection<Festival_Artiste> festival_Artistes = new List<Festival_Artiste>();
            HttpResponseMessage response = client.GetAsync("api/festival_Artistes").Result;
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
            HttpResponseMessage response = client.GetAsync("api/festival_Artistes/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                festival_Artiste = JsonConvert.DeserializeObject<Festival_Artiste>(resp);
            }
            return festival_Artiste;
        }

        public async Task<HttpResponseMessage> AjoutFestival_ArtisteAsync(Festival_Artiste festival_Artiste)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/festival_Artistes", festival_Artiste);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> ModifFestival_ArtisteAsync(Festival_Artiste festival_Artiste)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/festival_Artistes/" + festival_Artiste.Id, festival_Artiste);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> SupprFestival_ArtisteAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/festival_Artistes/" + id);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        
        

        public async Task<HttpResponseMessage> SupprFestival_OrganisateurAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/festival_Organisateurs/" + id);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

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

        public async Task<HttpResponseMessage> AjoutHebergementAsync(Hebergement hebergement)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/hebergements", hebergement);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> ModifHebergementAsync(Hebergement hebergement)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/hebergements/" + hebergement.IdH, hebergement);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> SupprHebergementAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/hebergements/" + id);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

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

        public async Task<HttpResponseMessage> AjoutJourAsync(Jour jour)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/jours", jour);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> ModifJourAsync(Jour jour)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/jours/" + jour.IdJ, jour);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> SupprJourAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/jours/" + id);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

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

        public async Task<HttpResponseMessage> AjoutOrganisateurAsync(Organisateur organisateur)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/organisateurs", organisateur);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> ModifOrganisateurAsync(Organisateur organisateur)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/organisateurs/" + organisateur.IdO, organisateur);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> SupprOrganisateurAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/organisateurs/" + id);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

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

        public async Task<HttpResponseMessage> AjoutSceneAsync(Scene scene)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/scenes", scene);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> ModifSceneAsync(Scene scene)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/scenes/" + scene.IdS, scene);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> SupprSceneAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/scenes/" + id);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

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

        public async Task<HttpResponseMessage> AjoutTarifAsync(Tarif tarif)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/tarifs", tarif);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> ModifTarifAsync(Tarif tarif)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/tarifs/" + tarif.IdT, tarif);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> SupprTarifAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/tarifs/" + id);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Type_Hebergement>> GetType_HebergementsAsync()
        {
            ICollection<Type_Hebergement> type_Hebergements = new List<Type_Hebergement>();
            HttpResponseMessage response = client.GetAsync("api/type_Hebergements").Result;
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

        public async Task<HttpResponseMessage> AjoutType_HebergementAsync(Type_Hebergement type_Hebergement)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/type_Hebergements", type_Hebergement);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<HttpResponseMessage> ModifType_HebergementAsync(Type_Hebergement type_Hebergement)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/type_Hebergements/" + type_Hebergement.IDTH, type_Hebergement);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

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

        public async Task<HttpResponseMessage> SupprType_HebergementAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/type_Hebergements/" + id);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        

    }
}
