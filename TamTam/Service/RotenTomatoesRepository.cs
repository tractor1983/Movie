using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamTam.Dto;

namespace TamTam.Service
{
    public class RotenTomatoesRepository : IMovieRepository
    {
        public IEnumerable<MovieEntry> Search(SearchQuery query)
        {
            return new List<MovieEntry>();
        }
    }
}