using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TamTam.Dto;

namespace TamTam.Service
{
    public interface ISearchService
    {
        IEnumerable<ResultEntry> Search(string query);
    }
}