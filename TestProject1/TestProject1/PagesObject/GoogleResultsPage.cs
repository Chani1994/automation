using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.PagesObject
{
    internal class GoogleResultsPage
    {
        private IWebDriver driver;
        private IList<IWebElement> results;
        ReadOnlyCollection<IWebElement> searchResults;
        private WebDriverWait wait;

        public GoogleResultsPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public bool ResultsDisplayed()
        {
            IWebElement resultsPanel = driver.FindElement(By.Id("search"));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(".//a")));
            searchResults = resultsPanel.FindElements(By.XPath(".//a"));
            return searchResults.Count() > 0;
        }
        public string GetFirstResultTitle()
        {
            return searchResults[0].FindElement(By.TagName("h3")).Text;
        }
        public void ClickFirstResult()
        {
            searchResults[0].FindElement(By.TagName("h3")).Click();
        }
    }
}

