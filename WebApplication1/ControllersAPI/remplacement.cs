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


        public async Task<ICollection<Ami>> GetAmisAsync()
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

        public async Task<Ami> GetAmiAsync(int? id)
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
