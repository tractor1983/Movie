using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TamTam.Dto;
using TamTam.Service;

namespace TamTam.Controllers
{
    public class SearchController : ApiController
    {
        public ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // GET api/<controller>/5
        public IEnumerable<ResultEntry> Get([FromUri]string q)
        {
            return searchService.Search(q);
        }

    }
}