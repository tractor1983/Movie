using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamTam.Dto;

namespace TamTam.Service
{
    public interface IMovieRepository
    {
        IEnumerable<MovieEntry> Search(SearchQuery query);
    }
}