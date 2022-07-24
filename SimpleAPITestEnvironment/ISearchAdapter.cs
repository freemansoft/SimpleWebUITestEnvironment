using Newtonsoft.Json.Linq;
using System.Net;

namespace SimpleAPITestEnvironment
{
    internal interface ISearchAdapter
    {
        public HttpWebResponse RunSearch(String query, bool logResponse);
        public Dictionary<String, String> GetVendorHeaders(bool logThem);
        internal IEnumerable<JToken> UrlsContaining(string queryDomainExpected, bool logWebPages);

    }
}
