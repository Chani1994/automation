using DemoQA.pagesObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace DemoQA
{
    [TestFixture]

    public class Tests
    {
        private IWebDriver driver;
        private AlertPage alertPage;
        private WindowPage windowPage;
        [SetUp]
        public void Setup()
        {
            string path = "C:\\Users\\user1\\Desktop\\TestProject1\\TestProject1\\driver";
            driver=new ChromeDriver(path);
            alertPage = new AlertPage(driver);
            windowPage = new WindowPage(driver);  

        }
        [Test]
        public void TestAlert()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/alerts");
            var alertButton = driver.FindElement(By.Id("confirmButton"));
            alertButton.Click();
            IAlert alert = WaitForAlert(driver, TimeSpan.FromSeconds(10));

            Assert.IsNotNull(alert, "alert was not displayed");
            alert.Accept();

            try
            {
                IWebElement element = driver.FindElement(By.Id("confirmResult"));
                Assert.IsNotNull(element, "Element with ID 'yourElementId' was not added.");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Element with ID 'yourElementId' was not found. Test failed.");
            }


        }

        [Test]
        public void TestSwichBetweenWindowsAndTabs()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/browser-windows");
            //HideAds();
            var windowButton = driver.FindElement(By.Id("windowButton"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", windowButton);

            string orginalWindow = driver.CurrentWindowHandle;

            WaitForNewWindow(driver,2);

            foreach(string windowHandle in driver.WindowHandles)
            {
                if(windowHandle!= orginalWindow)
                {
                    driver.SwitchTo().Window(windowHandle);
                    break;
                }
            }
            var newTabHeading = driver.FindElement(By.Id("sampleHeading"));
            Assert.AreEqual("This is a sample page", newTabHeading.Text);

            driver.Close();
            driver.SwitchTo().Window(orginalWindow);    
        }
        private IAlert WaitForAlert(IWebDriver driver, TimeSpan timeout)
        {
            try
            {

                WebDriverWait wait = new WebDriverWait(driver, timeout);
                return wait.Until(ExpectedConditions.AlertIsPresent());
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }
        private static void WaitForNewWindow(IWebDriver driver,int expecteWindowCount)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d=> d.WindowHandles.Count== expecteWindowCount);
        }
        //private void HideAds()
        //{

        //}
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}