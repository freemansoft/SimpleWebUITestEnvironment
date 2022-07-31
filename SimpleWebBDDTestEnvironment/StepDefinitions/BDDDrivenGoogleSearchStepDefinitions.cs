using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SimpleWebUITestEnvironment2.StepDefinitions
{
    [Binding]
    public class BDDDrivenGoogleSearchStepDefinitions
    {
        private ScenarioContext _scenarioContext;

        public BDDDrivenGoogleSearchStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario()]
        public void BeforeScenario()
        {
            IWebDriver driver = new ChromeDriver();
            _scenarioContext.Set<IWebDriver>(driver, "driver");
        }

        [AfterScenario()]
        public void AfterScenario()
        {
            IWebDriver driver = _scenarioContext.Get<IWebDriver>("driver");
            driver.Quit();
        }

        [Given(@"I search the internet using site ""([^""]*)""")]
        public void ISearchTheInternetUsingSite(string p0)
        {
            IWebDriver driver = _scenarioContext.Get<IWebDriver>("driver");
            driver.Should().NotBeNull();
            driver.Navigate().GoToUrl("https://www." + p0 + ".com");
        }

        /// <summary>
        /// Saves the last search term to be used in "Then" statements
        /// </summary>
        /// <param name="p0"></param>
        [When(@"I use the term ""([^""]*)""")]
        public void IUseTheTheTerm(string p0)
        {
            _scenarioContext.Set<string>(p0, "lastSearch");
            IWebDriver driver = _scenarioContext.Get<IWebDriver>("driver");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Name("q")));
            IWebElement queryField = driver.FindElement(By.Name("q"));
            queryField.SendKeys(p0);
            queryField.Submit();
            // wait until we get some link with p0 in it
            wait.Until(d => d.FindElement(By.PartialLinkText(p0))).Should().NotBeNull();
        }

        /// <summary>
        /// Assumes "search term " refers to last search term
        /// </summary>
        [Then(@"My search term should be in the title bar")]
        public void MySearchTermShouldBeInTheTitleBar()
        {
            string lastSearch = _scenarioContext.Get<string>("lastSearch");
            IWebDriver driver = _scenarioContext.Get<IWebDriver>("driver");
            string title = driver.Title;
            title.Should().Contain(lastSearch);

        }

        [Then(@"The first link takes me to a web site")]
        public void TheFirstLinkTakesMeToAWebSite()
        {
            string lastSearch = _scenarioContext.Get<string>("lastSearch");
            IWebDriver driver = _scenarioContext.Get<IWebDriver>("driver");
            IWebElement link = driver.FindElement(By.PartialLinkText(lastSearch));
            link.Click();
            // check against the title of the home page of the site we went to
            driver.Title.ToLower().Should().Contain(lastSearch.ToLower());
        }


        [Then(@"There should be at least (.*) links with the trademark holder site ""([^""]*)"" in them")]
        public void ThereShouldBeAtLeastLinksWithTheInThem(int linkCountMinimum, string domainName)
        {
            IWebDriver driver = _scenarioContext.Get<IWebDriver>("driver");

            IReadOnlyCollection<IWebElement> links = driver.FindElements(By.TagName("a")).ToList();
            //foreach (IWebElement e in links)
            //{
            //    System.Console.WriteLine(e.GetAttribute("href") + "    " + e.Text);
            //}
            // href can be blank in <a> tag.  Who knew?
            int numInDomain = links.Count(x => (x.GetAttribute("href") + " ").Contains(domainName));
            numInDomain.Should().BeGreaterThanOrEqualTo(linkCountMinimum);
        }

    }
}
