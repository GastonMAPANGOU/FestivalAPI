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
            client.BaseAddress = new Uri("http://localhost:44344");
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

    }
}
