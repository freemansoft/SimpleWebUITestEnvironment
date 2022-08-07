
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleAPITestEnvironment.SearchConfiguration;

namespace SimpleAPITestEnvironment.SearchAdapters
{
    public class BingSearchAdapter : ISearchAdapter
    {
        private string endpoint;
        private string subscriptionKey;


        private HttpResponseMessage responseMessage;

        private string responseJson;
        private JObject responseJObject;

        public BingSearchAdapter(ISearchEndpoint endpointConfig)
        {
            endpoint = endpointConfig.ApiEndpoint();
            subscriptionKey = endpointConfig.SubscriptionKey();
            responseJson = "";
            responseJObject = new JObject();
            responseMessage = null!;
        }

        public HttpResponseMessage RunSearch(string query, bool logResponse)
        {
            // Construct the URI of the search request
            var uriQuery = endpoint + "?q=" + Uri.EscapeDataString(query) + "&mkt=" + "en-US" + "&responseFilter=" + Uri.EscapeDataString("webpages");

            // Perform the Web request and get the response
            HttpClient ourClient = new HttpClient();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uriQuery),
                Method = HttpMethod.Get
            };
            request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            responseMessage = ourClient.Send(request);
            responseMessage.EnsureSuccessStatusCode();

            var readAsStringAsync = responseMessage.Content.ReadAsStringAsync();
            responseJson = readAsStringAsync.Result;

            responseJObject = JObject.Parse(responseJson);
            if (logResponse)
            {
                Console.WriteLine(JsonConvert.SerializeObject(responseJObject, Formatting.Indented));
            }
            return responseMessage;

        }

        public Dictionary<string, IEnumerable<string>> GetVendorHeaders(bool logThem)
        {
            if (responseMessage == null)
            {
                throw new NullReferenceException();
            }

            Dictionary<string, IEnumerable<string>> relevantHeaders = new Dictionary<string, IEnumerable<string>>();

            // Extract Bing HTTP headers
            foreach (KeyValuePair<string, IEnumerable<string>> header in responseMessage.Headers)
            {
                if (header.Key.StartsWith("BingAPIs-") || header.Key.StartsWith("X-MSEdge-"))
                    relevantHeaders.Add(header.Key, header.Value);
            }

            // Show headers
            if (logThem)
            {
                Console.WriteLine("Relevant HTTP Headers:");
                foreach (var header in relevantHeaders)
                {
                    Console.WriteLine("    " + header.Key + ": " + header.Value);
                }
            }
            return relevantHeaders;
        }

        public IEnumerable<JToken> UrlsContaining(string queryDomainExpected, bool logWebPages)
        {
            if (responseMessage == null)
            {
                throw new NullReferenceException();
            }

            JToken webPagesValues = responseJObject["webPages"]["value"];
            if (logWebPages)
            {
                Console.WriteLine("JSON Response: webpages.value");
                Console.WriteLine(webPagesValues);
            }


            return webPagesValues.Where(n => n.SelectToken("url").ToString().Contains(queryDomainExpected)).Select(i => i.SelectToken("url"));

        }
    }
}