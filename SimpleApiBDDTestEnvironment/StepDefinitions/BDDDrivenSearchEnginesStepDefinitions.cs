using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SimpleAPITestEnvironment;
using System.Net;


namespace SimpleApiBDDTestEnvironment.StepDefinitions
{
    [Binding]
    public class BDDDrivenSearchEnginesStepDefinitions
    {
        // Add your Bing Search V7 subscription key and endpoint to your environment variables - .runsettings if running in visual studio
        static string subscriptionKey = Environment.GetEnvironmentVariable("BING_SEARCH_V7_SUBSCRIPTION_KEY");
        static string endpoint = Environment.GetEnvironmentVariable("BING_SEARCH_V7_ENDPOINT") + "/v7.0/search";

        private ScenarioContext _scenarioContext;

        public BDDDrivenSearchEnginesStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I search the internet using site ""([^""]*)""")]
        public void GivenISearchTheInternetUsingSite(string site)
        {
            //We don't have a Google test class yet
            Assert.That(site, Is.EqualTo("bing"));
            _scenarioContext.Set<ISearchAdapter>(new BingSearchAdapter(endpoint, subscriptionKey));
        }

        [When(@"I use the term ""([^""]*)""")]
        public void WhenIUseTheTerm(string searchTerm)
        {
            ISearchAdapter adapter = _scenarioContext.Get<ISearchAdapter>();

            HttpWebResponse response = adapter.RunSearch(searchTerm, false);
            Assert.NotNull(response);
        }

        [Then(@"There should be at least (.*) links with the trademark holder site ""([^""]*)"" in them")]
        public void ThenThereShouldBeAtLeastLinksWithTheInThem(int numLinks, string linkSubstring)
        {
            ISearchAdapter adapter = _scenarioContext.Get<ISearchAdapter>();
            IEnumerable<JToken> urls = adapter.UrlsContaining(linkSubstring, true);
            Assert.GreaterOrEqual(urls.Count(), numLinks);
        }


    }
}
