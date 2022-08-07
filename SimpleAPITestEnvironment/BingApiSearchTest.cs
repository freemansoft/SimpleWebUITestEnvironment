///
/// Verifies the bing search library with a hard coded unit test..
///
namespace SimpleAPITestEnvironment
{
    using FluentAssertions;
    using Newtonsoft.Json.Linq;
    /// https://docs.microsoft.com/en-us/bing/search-apis/bing-web-search/quickstarts/rest/csharp

    using System;
    using System.Collections.Generic;
    using System.Text;
    public class BingApiSearchTest
    {
        const string query = "facebook";
        const string queryDomainExpected = "www.facebook.com";

        ISearchAdapter adapter;

        ISearchEndpoint endpointConfig;

        /// <summary>
        /// hardwired to bing right now
        /// </summary>
        [SetUp]
        public void Setup()
        {
            endpointConfig = new BingSearchEndpoint();
            adapter = new BingSearchAdapter(endpointConfig);

        }

        /// <summary>
        /// Hard coded test that runs against facebook using the bing library
        /// </summary>
        [Test]
        public void SearchBingForFacebook()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Searching the Web for: " + query);

            HttpResponseMessage response = adapter.RunSearch(query, false);
            // Create a dictionary to store relevant headers and have the utility log them
            Dictionary<String, IEnumerable<string>> relevantHeaders = adapter.GetVendorHeaders(true);

            // select the objects that have the desired domain and then pull out the url itself.
            IEnumerable<JToken> urls = adapter.UrlsContaining(queryDomainExpected, true);
            Console.WriteLine("JSON Response: matching urls");
            foreach (JToken url in urls)
            {
                Console.WriteLine(url);
            }
            urls.Count().Should().BeGreaterThan(0);
        }


    }
}