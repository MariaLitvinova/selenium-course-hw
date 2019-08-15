using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace LitecartTesting.Pages
{
    public class MainStorePage
    {
        private readonly IWebDriver webDriver;
        private readonly WebDriverWait wait;

        private readonly (string sectionName, List<(string duckName, string duckLabel)> sectionContent) expectedMostPopularContent =
            ("Most Popular", new List<(string, string)>()
            {
                ("Green Duck", "NEW"),
                ("Purple Duck", "NEW"),
                ("Red Duck", "NEW"),
                ("Blue Duck", "NEW"),
                ("Yellow Duck", "SALE"),
            });

        private readonly (string sectionName, List<(string duckName, string duckLabel)> sectionContent) expectedCampaignsContent =
            ("Campaigns", new List<(string, string)>()
            {
                ("Yellow Duck", "SALE")
            });

        private readonly (string sectionName, List<(string duckName, string duckLabel)> sectionContent) expectedLatestProductsContent =
            ("Latest Products", new List<(string, string)>()
            {
                ("Yellow Duck", "SALE"),
                ("Green Duck", "NEW"),
                ("Red Duck", "NEW"),
                ("Blue Duck", "NEW"),
                ("Purple Duck", "NEW")
            });

        private void CheckLabel(IWebElement duckElement, string labelText)
        {
            var imageWrapper = duckElement.FindElements(By.ClassName("image-wrapper")).FirstOrDefault();
            var labelElements = imageWrapper.FindElements(By.TagName("div"));
            Assert.AreEqual(1, labelElements.Count);
            Assert.AreEqual(labelText, labelElements.First().Text);
        }

        private void CheckDucksInSection(
            IWebElement section, 
            (string sectionTitle, List<(string duckName, string duckLabel)> sectionContent) expectedContent)
        {
            var title = section.FindElement(By.ClassName("title")).Text;
            Assert.AreEqual(expectedContent.sectionTitle, title);

            var allDucks = GetAllDucksInSection(section);
            Assert.AreEqual(expectedContent.sectionContent.Count, allDucks.Count);

            for (int i = 0; i < allDucks.Count; ++i)
            {
                var actualDuck = allDucks.ElementAt(i);
                var actualDuckName = actualDuck.FindElement(By.ClassName("name")).Text;
                var expectedDuck = expectedContent.sectionContent
                    .Where(x => x.duckName == actualDuckName)
                    .FirstOrDefault();
                Assert.IsNotNull(expectedDuck);

                CheckLabel(actualDuck, expectedDuck.duckLabel);
            }
        }

        public IWebElement MostPopularSection => webDriver.FindElement(By.Id("box-most-popular"));

        public IWebElement CampaignsSection => webDriver.FindElement(By.Id("box-campaigns"));

        public IWebElement LatestProducts => webDriver.FindElement(By.Id("box-latest-products"));

        public IReadOnlyCollection<IWebElement> GetAllDucksInSection(IWebElement section) 
            => section.FindElements(By.TagName("li"));

        public MainStorePage(IWebDriver webDriver, WebDriverWait wait)
        {
            this.webDriver = webDriver;
            this.wait = wait;
        }

        public void Load()
        {
            webDriver.Url = "http://localhost/litecart/en/";

            wait.Until(driver => driver.Title.Contains("Online Store"));
        }

        public void CheckDuckLabels()
        {
            CheckDucksInSection(MostPopularSection, expectedMostPopularContent);
            CheckDucksInSection(CampaignsSection, expectedCampaignsContent);
            CheckDucksInSection(LatestProducts, expectedLatestProductsContent);
        }
    }
}
