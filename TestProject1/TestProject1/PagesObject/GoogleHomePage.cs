using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.PagesObject
{
    internal class GoogleHomePage
    {
        private IWebDriver driver;
        public GoogleHomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement SearchBox => driver.FindElement(By.Name("q"));
        private IWebElement SearchButton => driver.FindElement(By.Name("btnk"));

        public void NavigateTo()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
        }

        public void Search(string query)
        {
            SearchBox.SendKeys(query);
            SearchBox.Submit();
        }
    }
}

