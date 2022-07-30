using Newtonsoft.Json.Linq;
using System.Net;

namespace SimpleAPITestEnvironment
{
    public interface ISearchAdapter
    {
        public HttpResponseMessage RunSearch(String query, bool logResponse);
        public Dictionary<String, IEnumerable<string>> GetVendorHeaders(bool logThem);
        public IEnumerable<JToken> UrlsContaining(string queryDomainExpected, bool logWebPages);

    }
}
