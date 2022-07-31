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
        // Add your Bing Search V7 subscription key and endpoint to your environment variables - .runsettings if running in visual studio
        static string subscriptionKey = Environment.GetEnvironmentVariable("BING_SEARCH_V7_SUBSCRIPTION_KEY") ?? "";
        static string endpoint = Environment.GetEnvironmentVariable("BING_SEARCH_V7_ENDPOINT") + "/v7.0/search";

        const string query = "facebook";
        const string queryDomainExpected = "www.facebook.com";


        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Hard coded test that runs against facebook using the bing library
        /// </summary>
        [Test]
        public void SearchBingForFacebook()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Searching the Web for: " + query);

            ISearchAdapter adapter = new BingSearchAdapter(endpoint, subscriptionKey);
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