using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tests.Selenium
{
    /// <summary>
    /// Summary description for StandaloneTest
    /// </summary>
    [TestClass]
    public class GoogleSeleniumUnitTest
    {
        private IWebDriver Driver;
        private WebDriverWait WaitSupport;

        /// <summary>
        /// Initialize the Selenium web Driver once per test to totally isolate the tests.
        /// You could do this once per class with better performance at the risk of
        /// more cross test dependencies.
        /// </summary>
        /// <param name="testContext">standard test context</param>
        [TestInitialize()]
        public void MyTestInitialize()
        {
            Driver = new ChromeDriver();
            WaitSupport = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Quit the Driver so that tests classes can't impact each other.
        /// Each test class creates its own Driver.
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup()
        {
            Driver.Quit();
        }


        /// <summary>
        /// Simple method that searches on google and verifies that the search term is in the title bar
        /// </summary>
        [TestMethod]
        public void TestGoogleSearch()
        {
            Driver.Navigate().GoToUrl("http://www.google.com");
            IWebElement queryField = Driver.FindElement(By.Name("q"));
            queryField.SendKeys("freemansoft");
            queryField.Submit();
            // WaitSupport until a link for the search term shows up before doing anything else
            // google links don't have ids or names
            Assert.IsNotNull(WaitSupport.Until(d => d.FindElement(By.PartialLinkText("Joe Freeman"))));
            // google puts search term in title bar after search
            StringAssert.Contains(Driver.Title, "freemansoft");
            // find the link again and click on it to go to the home page
            IWebElement link = Driver.FindElement(By.PartialLinkText("joe.blog.freemansoft.com"));
            link.Click();
            // check against the title of the home page of the site we went to
            StringAssert.Contains(Driver.Title, "Joe Freeman");
        }
    }
}
