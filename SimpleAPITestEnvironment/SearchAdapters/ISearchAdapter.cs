using Newtonsoft.Json.Linq;

namespace SimpleAPITestEnvironment.SearchAdapters
{
    /// <summary>
    /// Class defines context reqired to run all of these tests
    /// </summary>
    public interface ISearchAdapter
    {
        public HttpResponseMessage RunSearch(string query, bool logResponse);
        public Dictionary<string, IEnumerable<string>> GetVendorHeaders(bool logThem);
        public IEnumerable<JToken> UrlsContaining(string queryDomainExpected, bool logWebPages);

    }
}
