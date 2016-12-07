using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace TamTam.Controllers
{
    //used to bypass chrome security
    public class FileController : ApiController
    {
        public Task<HttpResponseMessage> Get([FromUri]string f)
        {

            var range = ExtractRange(this.Request.Headers);
            var request = new HttpRequestMessage(HttpMethod.Get, f);


            var client = new HttpClient();
            var response = client.SendAsync(request).ContinueWith<HttpResponseMessage>(t =>
            {
                var finalResp = new HttpResponseMessage(HttpStatusCode.OK);
                finalResp.Content = t.Result.Content;
                if (null != range) finalResp.StatusCode = HttpStatusCode.PartialContent;
                return finalResp;
            });

            return response;
        }

        private static RangeHeaderValue ExtractRange(HttpRequestHeaders headers)
        {
            if (null == headers)
                throw new ArgumentNullException("headers");

            const int readStreamBufferSize = 1024 * 1024;
            var hasRange = (null != headers.Range && headers.Range.Ranges.Any());
            var rangeHeader = hasRange ? headers.Range : new RangeHeaderValue(0, readStreamBufferSize);
            if (!hasRange) return rangeHeader;
            // it is better to limit the request to a specific range in order to do no have an out-of-memory exception		    
            var range = rangeHeader.Ranges.ElementAt(0);
            var from = range.From.GetValueOrDefault(0);

            rangeHeader = new RangeHeaderValue(@from, @from + range.To.GetValueOrDefault(readStreamBufferSize));
            return rangeHeader;		
        }
    }
}
