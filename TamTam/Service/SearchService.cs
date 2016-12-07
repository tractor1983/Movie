using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using TamTam.Dto;

namespace TamTam.Service
{
    public class SearchService: ISearchService
    {
        private IEnumerable<IMovieRepository> movieSearchersRepo;
        private IEnumerable<IVideoRepository> trailerSearchersRepo;
        
        public SearchService(IEnumerable<IMovieRepository> movieSearchersRepo, IEnumerable<IVideoRepository> trailerSearchersRepo)
        {
            this.movieSearchersRepo = movieSearchersRepo;
            this.trailerSearchersRepo = trailerSearchersRepo;
        }

        IEnumerable<ResultEntry> ISearchService.Search(string query)
        {
            List<ResultEntry> movies = SearchMovies(query);
            FillTrailers(movies);

            return movies;
        }

        private void FillTrailers(List<ResultEntry> movies)
        {
            Parallel.ForEach(movies, x =>
            {
                var temp = trailerSearchersRepo
                .AsParallel()
                .SelectMany(searcher => searcher.Search(new SearchQuery
                {
                    Title = ComposeVideoSearchQuery(x)
                })).ToList();

                x.Videos = temp;
            });
        }

        private List<ResultEntry> SearchMovies(string query)
        {
            return movieSearchersRepo
                            .AsParallel()
                            .SelectMany(x => x.Search(new SearchQuery
                            {
                                Title = ComposeMovieSearchQuery(query)
                            }))
                            .Select(x => new ResultEntry
                            {
                                MovieTitle = x.Title,
                                Released = x.Released,
                                Year = x.Year,
                                Rating = x.ImdbRating,
                                PosterUrl = x.PosterUrl                            
                            }).ToList();
        }

        private string ComposeMovieSearchQuery(string serachQuery)
        {
            //remove trailer form the movie
            var videoQuery = Regex.Replace(serachQuery, "trailer", "", RegexOptions.IgnoreCase);

            //remove year ad put it at the end. eg: "2007 The fly" -> "The fly 2007" 
            Regex pattern = new Regex(@"^(19|20)\d{2}");

            Match match = pattern.Match(serachQuery);
            if (!string.IsNullOrWhiteSpace(match.Value))
            {
                videoQuery = videoQuery.Replace(match.Value, "");
            }

            videoQuery = $"{videoQuery} {match.Value}";

            return videoQuery;
        }

        private string ComposeVideoSearchQuery(ResultEntry entry)
        {
            return $"{entry.MovieTitle} {entry.Year} trailer"; 
        }
    }
}