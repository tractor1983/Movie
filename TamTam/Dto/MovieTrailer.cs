using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamTam.Dto
{
    public class MovieEntry
    {
        public string Title { get; set; }
        public string Year { get; set; }

        [JsonProperty("imdbRating")]
        public string ImdbRating { get; set; }
        public string Released { get; set; }
        public string PosterUrl { get; set; }
    }
}