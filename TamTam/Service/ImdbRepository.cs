using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TamTam.Dto;

namespace TamTam.Service
{
    public class ImdbRepository : IMovieRepository
    {
        public IEnumerable<MovieEntry> Search(SearchQuery query)
        {
            ImdbResponse movies = GetMovieStubs(query);

            var moviesToSearch = movies.Search.Take(3).ToList();

            var movieList = PopulateMovies(moviesToSearch);

            return movieList;
        }

        private List<MovieEntry> PopulateMovies(List<SearchEntry> movieEntries)
        {
            var movies = new List<MovieEntry>();

            Parallel.ForEach(movieEntries, x =>
            {
                var movie = PopulateMovie(x);
                movies.Add(movie);
            });

            return movies;
        }

        private MovieEntry PopulateMovie(SearchEntry movieEntry)
        {
            var client = new RestClient("http://www.omdbapi.com");
            var request = new RestRequest($"?i={movieEntry.ImdbID}&r=json", Method.GET);

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            var movie = JsonConvert.DeserializeObject<MovieEntry>(content);

            if (movie == null)
            {
                return new MovieEntry
                {
                    Title = movieEntry.Title,
                    Year = movieEntry.Year,
                    ImdbRating = movie.ImdbRating,
                    PosterUrl = movie.PosterUrl,
                    Released = movie.Released
                };
            }
            
            movie.PosterUrl = movieEntry.Poster;
            return movie;
        }

        private ImdbResponse GetMovieStubs(SearchQuery query)
        {
            var client = new RestClient("http://www.omdbapi.com");
            var request = new RestRequest($"?s={query.Title.ToLower().Trim()}&r=json&page=1", Method.GET);

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            var movie = JsonConvert.DeserializeObject<ImdbResponse>(content);
            return movie;
        }

        private class ImdbResponse
        {
            public List<SearchEntry> Search { get; set; }            
        }

        private class SearchEntry
        {
            public string Title { get; set; }
            public string Year { get; set; }
            public string Poster { get; set; }
            public string Released { get; set; }

            [JsonProperty("imdbID")]
            public string ImdbID { get; set; }
        }

    }
}