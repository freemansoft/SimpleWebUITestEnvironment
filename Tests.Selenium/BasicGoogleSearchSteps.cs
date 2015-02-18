using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace Tests.Selenium
{
    [Binding]
    public class BasicGoogleSearchSteps
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            IWebDriver driver = new ChromeDriver();
            ScenarioContext.Current.Set<IWebDriver>(driver, "driver");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            IWebDriver driver = ScenarioContext.Current.Get<IWebDriver>("driver");
            driver.Quit();
        }

        [Given(@"I want to search with Google")]
        public void GivenIWantToSearchWithGoogle()
        {
            IWebDriver driver = ScenarioContext.Current.Get<IWebDriver>("driver");
            driver.Navigate().GoToUrl("http://www.google.com");
        }

        /// <summary>
        /// Saves the last search term to be used in "Then" statements
        /// </summary>
        /// <param name="p0"></param>
        [When(@"When I search for ""(.*)""")]
        public void WhenWhenISearchFor(string p0)
        {
            ScenarioContext.Current.Set<string>(p0, "lastSearch");
            IWebDriver driver = ScenarioContext.Current.Get<IWebDriver>("driver");
            IWebElement queryField = driver.FindElement(By.Name("q"));
            queryField.SendKeys(p0);
            queryField.Submit();
            // how do we handle various delays hitting external web site?
            Thread.Sleep(3000);
        }

        /// <summary>
        /// Assumes "That" refers to last search term
        /// </summary>
        [Then(@"That should be in the title bar")]
        public void ThatShouldBeInTheTitleBar()
        {
            string lastSearch = ScenarioContext.Current.Get<string>("lastSearch");
            IWebDriver driver = ScenarioContext.Current.Get<IWebDriver>("driver");
            string title = driver.Title;
            StringAssert.Contains(title, lastSearch);
        }
    }
}
