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

    }
}
