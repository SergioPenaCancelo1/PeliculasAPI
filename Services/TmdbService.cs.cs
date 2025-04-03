using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using MovieServiceAPI.Models;

namespace MovieServiceAPI.Services
{
    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        // Constructor que obtiene la API Key desde appsettings.json
        public TmdbService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["TMDb:ApiKey"];
        }

        // Método para obtener información de una película por su título
        public async Task<Movie> GetMovieByTitleAsync(string title)
        {
            // Construye la URL de la API de TMDb para buscar la película
            string searchUrl = $"https://api.themoviedb.org/3/search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(title)}";

            // Hace la petición GET a la API
            HttpResponseMessage response = await _httpClient.GetAsync(searchUrl);

            if (!response.IsSuccessStatusCode)
                return null; // Retorna null si la API falla

            // Lee y parsea el JSON de la respuesta
            string jsonResponse = await response.Content.ReadAsStringAsync();
            JObject searchResult = JObject.Parse(jsonResponse);

            // Obtiene la primera película de los resultados
            JToken firstMovie = searchResult["results"]?.First;
            if (firstMovie == null)
                return null; // Retorna null si no hay resultados

            int movieId = firstMovie["id"].Value<int>(); // Obtiene el ID de la película
            return await GetMovieDetailsAsync(movieId); // Obtiene los detalles de la película
        }

        // Método para obtener detalles de una película por su ID
        private async Task<Movie> GetMovieDetailsAsync(int movieId)
        {
            string detailsUrl = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={_apiKey}";
            HttpResponseMessage response = await _httpClient.GetAsync(detailsUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            string jsonResponse = await response.Content.ReadAsStringAsync();
            JObject movieData = JObject.Parse(jsonResponse);

            // Crea el objeto Movie con la información obtenida
            var movie = new Movie
            {
                Title = movieData["title"].ToString(),
                OriginalTitle = movieData["original_title"].ToString(),
                Rating = movieData["vote_average"].Value<double>(),
                ReleaseDate = movieData["release_date"].ToString(),
                Overview = movieData["overview"].ToString(),
                SimilarMovies = await GetSimilarMoviesAsync(movieId) // Obtiene películas similares
            };

            return movie;
        }

        // Método para obtener películas similares
        private async Task<List<string>> GetSimilarMoviesAsync(int movieId)
        {
            string similarUrl = $"https://api.themoviedb.org/3/movie/{movieId}/similar?api_key={_apiKey}";
            HttpResponseMessage response = await _httpClient.GetAsync(similarUrl);

            if (!response.IsSuccessStatusCode)
                return new List<string>();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            JObject similarData = JObject.Parse(jsonResponse);

            // Extrae hasta 5 títulos de películas similares
            List<string> similarMovies = new();
            foreach (var movie in similarData["results"].Take(5))
            {
                string title = $"{movie["title"]} ({movie["release_date"].ToString().Split('-')[0]})";
                similarMovies.Add(title);
            }

            return similarMovies;
        }
    }
}
