using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamTam.Dto;

namespace TamTam.Service
{
    public class VimeoRepository: IVideoRepository
    {
        public IEnumerable<VideoEntry> Search(SearchQuery query)
        {
            return new List<VideoEntry>();
        }
    }
}