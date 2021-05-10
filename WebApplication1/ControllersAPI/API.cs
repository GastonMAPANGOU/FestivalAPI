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

        public async Task<Uri> AjoutFestival_ArtisteAsync(Festival_Artiste festival_Artiste)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/festival_Artistes", festival_Artiste);
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
                HttpResponseMessage response = await client.PutAsJsonAsync("api/festival_Artistes/" + festival_Artiste.Id, festival_Artiste);
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
                HttpResponseMessage response = await client.DeleteAsync("api/festival_Artistes/" + id);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
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

            while (jour.Date_jour<festival.Date_Fin)
            {
                
                jour.Numero_jour = "Jour" + i;
                await AjoutJourAsync(jour);
                jour.Date_jour=jour.Date_jour.AddDays(1);
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

        public async Task<Uri> AjoutType_HebergementAsync(Type_Hebergement type_Hebergement)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/type_Hebergements", type_Hebergement);
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
                HttpResponseMessage response = await client.PutAsJsonAsync("api/type_Hebergements/" + type_Hebergement.IDTH, type_Hebergement);
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
                HttpResponseMessage response = await client.DeleteAsync("api/type_Hebergements/" + id);
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

        public async Task<Ami> GetAmitiéAsync(int idami1,int idami2)
        {
            Ami ami = null;
            HttpResponseMessage response = client.GetAsync("api/amis/" + idami1+"/"+ idami2).Result;
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
    }
}
