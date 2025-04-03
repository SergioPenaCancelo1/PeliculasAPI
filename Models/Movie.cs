using System.Collections.Generic;

namespace MovieServiceAPI.Models
{
    // Representa la información de una película
    public class Movie
    {
        public string Title { get; set; }  // Título en el idioma del usuario
        public string OriginalTitle { get; set; }  // Título original de la película
        public double Rating { get; set; }  // Puntuación media
        public string ReleaseDate { get; set; }  // Fecha de estreno
        public string Overview { get; set; }  // Descripción / sinopsis
        public List<string> SimilarMovies { get; set; }  // Lista de títulos de películas similares
    }
}