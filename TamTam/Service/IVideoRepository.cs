using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamTam.Dto;

namespace TamTam.Service
{
    public interface IVideoRepository
    {
        IEnumerable<VideoEntry> Search(SearchQuery query);
    }
}