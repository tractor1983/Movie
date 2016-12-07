using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamTam.Dto
{
    public class ResultEntry
    {
        public IEnumerable<VideoEntry> Videos { get; set; }
        public string MovieTitle { get; set; }       
        public string Year { get; set; }
        public string Rating { get; set; }
        public string Released { get; set; }
        public string PosterUrl { get; set; }
    }
}