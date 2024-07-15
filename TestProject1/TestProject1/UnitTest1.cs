using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;
using TestProject1.PagesObject;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private GoogleHomePage googleHomePage;
        private GoogleResultsPage googleResultsPage;

        public static IEnumerable<TestData> TestCases => XmlDataReader.ReadTestData("C:\\Users\\user1\\Desktop\\TestProject1\\TestProject1\\TestData.xml");
        public Tests() { }
        [SetUp]
        public void Setup()
        {
            string path = "C:\\Users\\user1\\Desktop\\TestProject1\\TestProject1\\driver";
            driver = new ChromeDriver(path);
            googleHomePage = new GoogleHomePage(driver);
            googleResultsPage = new GoogleResultsPage(driver);
            //  driver.Manage().Window.Maximize();
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public void TestGoogleSearch(TestData testData)
        {
            googleHomePage.NavigateTo();


            Assert.AreEqual("Google", driver.Title);

            googleHomePage.Search(testData.SearchTerm);
            googleHomePage.Search("Selenium WebDriver");


            //IWebElement searchBox = driver.FindElement(By.Name("q"));
            //searchBox.SendKeys("Selenium WebDriver");
            //searchBox.Submit();

            Assert.IsTrue(googleResultsPage.ResultsDisplayed());

            string firstResultTitle = googleResultsPage.GetFirstResultTitle();

            googleResultsPage.ClickFirstResult();

            Assert.IsTrue(driver.Title.Contains(firstResultTitle));
            driver.Navigate().Back();
            Assert.AreEqual("Selenium WebDriver", driver.FindElement(By.Name("q")).GetAttribute("value"));


        }
        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}