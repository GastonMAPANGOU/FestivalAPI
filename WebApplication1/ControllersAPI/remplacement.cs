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
    public sealed class remplacement
    {
        private static readonly HttpClient client = new HttpClient();

        private remplacement()
        {
            client.BaseAddress = new Uri("http://localhost:51179");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        private static readonly object padlock = new object();
        private static remplacement instance = null;
        
        public static remplacement Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new remplacement();
                    }
                    return instance;
                }
            }
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

    }
}
