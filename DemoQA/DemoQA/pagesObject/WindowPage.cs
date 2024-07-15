using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.pagesObject
{
    public class WindowPage
    {
        private IWebDriver driver;
        public WindowPage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
