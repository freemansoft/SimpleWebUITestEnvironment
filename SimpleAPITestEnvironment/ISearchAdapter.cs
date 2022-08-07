using Newtonsoft.Json.Linq;

namespace SimpleAPITestEnvironment
{
    /// <summary>
    /// Class defines context reqired to run all of these tests
    /// </summary>
    public interface ISearchAdapter
    {
        public HttpResponseMessage RunSearch(String query, bool logResponse);
        public Dictionary<String, IEnumerable<string>> GetVendorHeaders(bool logThem);
        public IEnumerable<JToken> UrlsContaining(string queryDomainExpected, bool logWebPages);

    }
}
