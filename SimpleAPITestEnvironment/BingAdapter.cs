using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace SimpleAPITestEnvironment
{
    public class BingAdapter : ISearchAdapter
    {
        private string endpoint;
        private string subscriptionKey;

        private HttpWebResponse response;
        private string responseJson;
        private JObject responseJObject;

        public BingAdapter(string endpoint, string subscriptionKey)
        {
            this.endpoint = endpoint;
            this.subscriptionKey = subscriptionKey;
        }

        public HttpWebResponse RunSearch(String query, bool logResponse)
        {
            // Construct the URI of the search request
            var uriQuery = endpoint + "?q=" + Uri.EscapeDataString(query) + "&mkt=" + "en-US" + "&responseFilter=" + Uri.EscapeDataString("webpages");

            // Perform the Web request and get the response
            WebRequest request = HttpWebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;
            response = (HttpWebResponse)request.GetResponseAsync().Result;

            responseJson = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (logResponse)
            {
                Console.WriteLine("JSON Response:");
                dynamic parsedJson = JsonConvert.DeserializeObject(responseJson);
                Console.WriteLine(JsonConvert.SerializeObject(parsedJson, Formatting.Indented));
            }

            responseJObject = JObject.Parse(responseJson);

            return response;
        }

        public Dictionary<String, String> GetVendorHeaders(bool logThem)
        {
            if (response == null)
            {
                throw new NullReferenceException();
            }

            Dictionary<String, String> relevantHeaders = new Dictionary<String, String>();

            // Extract Bing HTTP headers
            foreach (String header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    relevantHeaders[header] = response.Headers[header];
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
            if (response == null)
            {
                throw new NullReferenceException();
            }

            responseJObject = JObject.Parse(responseJson);
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