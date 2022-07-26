using Newtonsoft.Json.Linq;
using System.Net;

namespace SimpleAPITestEnvironment
{
    public interface ISearchAdapter
    {
        public HttpWebResponse RunSearch(String query, bool logResponse);
        public Dictionary<String, String> GetVendorHeaders(bool logThem);
        public IEnumerable<JToken> UrlsContaining(string queryDomainExpected, bool logWebPages);

    }
}
