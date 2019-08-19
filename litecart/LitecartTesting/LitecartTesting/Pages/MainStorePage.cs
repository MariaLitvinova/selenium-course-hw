using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace LitecartTesting.Pages
{
    public class MainStorePage : PageBase
    {
        private void CheckLabel(IWebElement duckElement)
        {
            var labelElements = duckElement.FindElements(By.ClassName("sticker"));

            Assert.AreEqual(1, labelElements.Count);
        }

        public ReadOnlyCollection<IWebElement> DucksList 
            => webDriver.FindElements(By.ClassName("product"));

        public MainStorePage(IWebDriver webDriver, WebDriverWait wait) : base(webDriver, wait) { }

        public void Load()
        {
            webDriver.Url = "http://localhost/litecart/en/";

            wait.Until(driver => driver.Title.Contains("Online Store"));
        }

        public void CheckDuckLabels()
        {
            Assert.AreEqual(11, DucksList.Count);

            for (int i = 0; i < DucksList.Count; ++i)
            {
                CheckLabel(DucksList[i]);
            }
        }
    }
}
