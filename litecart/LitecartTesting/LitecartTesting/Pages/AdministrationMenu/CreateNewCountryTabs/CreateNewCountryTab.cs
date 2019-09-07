using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Linq;

namespace LitecartTesting.Pages.AdministrationMenu
{
    public class CreateNewCountryTab : PageBase
    {
        private readonly string[] expectedWindowTitles = new string[]
        {
            "ISO 3166-1 alpha-2",
            "ISO 3166-1 alpha-3",
            "Regular expression",
            "International Address Format Validator",
            "Regular expression",
            "List of countries and capitals with currency and language",
            "List of country calling codes"
        };

        public ReadOnlyCollection<IWebElement> ExternalLinks
            => webDriver.FindElements(By.CssSelector("i.fa.fa-external-link"));

        public CreateNewCountryTab(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public void ClickOnAllExternalLinks()
        {
            WaitUntilHeaderAppears("Add New Country");
            Assert.AreEqual(7, ExternalLinks.Count);

            for (int i = 0; i < ExternalLinks.Count; ++i)
            {
                Assert.AreEqual(1, webDriver.WindowHandles.Count);
                var originalWindowHandle = webDriver.CurrentWindowHandle;

                ExternalLinks[i].Click();

                wait.Until(driver => driver.WindowHandles.Count == 2);
                var currentWindowHandles = webDriver.WindowHandles;
                var newWindowHandle = currentWindowHandles.Single(x => !string.Equals(x, originalWindowHandle));

                webDriver.SwitchTo().Window(newWindowHandle);

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(expectedWindowTitles[i]));

                webDriver.Close();
                webDriver.SwitchTo().Window(originalWindowHandle);

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Add New Country"));
            }
        }
    }
}
