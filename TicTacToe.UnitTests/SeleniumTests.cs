using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace TicTacToe.UnitTests
{

    [TestFixture]
    public class SeleniumTests
    {
        private IWebDriver _driver;
        [SetUp]
        public void SetupTest()
        {
            _driver = new ChromeDriver("D:/Kamil");
            INavigation nav = _driver.Navigate();
            nav.GoToUrl("http://www.google.com/");
            _driver.Manage().Window.Maximize();

        }

        [Test]
        public void GoogleSearch()
        {

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            IWebElement input = _driver.FindElement(By.Id("q"));
            input.SendKeys("Selenium");
            input.SendKeys(Keys.Enter);
            IWebElement searchBtn = _driver.FindElement(By.Name("btnK"));
            searchBtn.Click();
            string result = _driver.FindElement(By.ClassName("LC20lb")).Text;
            Assert.AreEqual("Selenium - Web Browser Automation", result);
        }

        [TearDown]

        public void Dispose()
        {
            _driver.Close();
        }
    }

}