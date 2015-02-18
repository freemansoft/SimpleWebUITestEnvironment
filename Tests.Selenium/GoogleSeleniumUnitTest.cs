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
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            driver = new ChromeDriver();
        }

        //
        // Use ClassCleanup to run code after all tests in a class have run
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
