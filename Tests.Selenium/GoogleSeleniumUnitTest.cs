using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;

namespace Tests.Selenium
{
    /// <summary>
    /// Summary description for StandaloneTest
    /// </summary>
    [TestClass]
    public class GoogleSeleniumUnitTest
    {
        public GoogleSeleniumUnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        static IWebDriver driver;

        #region Additional test attributes
        /// <summary>
        /// Initialize the Selenium web driver once per class.
        /// You could do this once per test in a TestInitialize method to improve
        /// isolation at the cost of additional processor and wall time.
        /// </summary>
        /// <param name="testContext">standard test context</param>
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            driver = new ChromeDriver();
        }

        /// <summary>
        /// Quit the driver so that tests classes can't impact each other.
        /// Each test class creates its own driver.
        /// </summary>
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            driver.Quit();
        }

        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        /// Simple method that searches on google and verifies that the search term is in the title bar
        /// </summary>
        [TestMethod]
        public void TestGoogleSearch()
        {
            driver.Navigate().GoToUrl("http://www.google.com");
            IWebElement queryField = driver.FindElement(By.Name("q"));
            queryField.SendKeys("freemansoft");
            queryField.Submit();
            // how do we handle various delays hitting external web site
            Thread.Sleep(3000);
            StringAssert.Contains(driver.Title, "freemansoft");
        }
    }
}
