using Newtonsoft.Json.Linq;
using SimpleAPITestEnvironment;

namespace SimpleApiBDDTestEnvironment.StepDefinitions
{
    [Binding]
    public class BDDDrivenSearchEnginesStepDefinitions
    {
        private ScenarioContext _scenarioContext;
        private ISearchEndpoint _endpointConfig;

        public BDDDrivenSearchEnginesStepDefinitions(ScenarioContext scenarioContext, BingSearchEndpoint endpointConfig)
        {
            _scenarioContext = scenarioContext;
            _endpointConfig = endpointConfig;
        }

        [Given(@"I search the internet using site ""([^""]*)""")]
        public void GivenISearchTheInternetUsingSite(string site)
        {
            //We don't have a Google test class yet - this should be a factory
            site.Should().Be("bing");
            _scenarioContext.Set<ISearchAdapter>(new BingSearchAdapter(_endpointConfig));
        }

        [When(@"I use the term ""([^""]*)""")]
        public void WhenIUseTheTerm(string searchTerm)
        {
            ISearchAdapter adapter = _scenarioContext.Get<ISearchAdapter>();

            HttpResponseMessage response = adapter.RunSearch(searchTerm, false);
            response.Should().NotBeNull();
        }

        [Then(@"There should be at least (.*) links with the trademark holder site ""([^""]*)"" in them")]
        public void ThenThereShouldBeAtLeastLinksWithTheInThem(int numLinks, string linkSubstring)
        {
            ISearchAdapter adapter = _scenarioContext.Get<ISearchAdapter>();
            IEnumerable<JToken> urls = adapter.UrlsContaining(linkSubstring, true);
            urls.Count().Should().BeGreaterThanOrEqualTo(numLinks);
        }


    }
}
