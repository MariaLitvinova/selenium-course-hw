using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace LitecartTesting.Pages.AdministrationMenu
{
    public class CountriesPage
    {
        private readonly IWebDriver webDriver;
        private readonly WebDriverWait wait;

        public IWebElement CountriesTable => webDriver.FindElement(By.ClassName("dataTable"));

        public ReadOnlyCollection<IWebElement> CountriesList => CountriesTable.FindElements(By.CssSelector("tr .row"));

        public CountriesPage(IWebDriver driver, WebDriverWait wait)
        {
            webDriver = driver;
            this.wait = wait;
        }

        public void CheckSorting()
        {
            bool headerAppeared = wait.Until(driver =>
                driver.FindElements(By.CssSelector("td#content h1")).Count == 1
                && driver.FindElement(By.CssSelector("td#content h1")).Text == "Countries");
            Assert.IsTrue(headerAppeared);

            var previousName = "";
            var currentName = "";

            foreach (var row in CountriesList)
            {
                var columns = row.FindElements(By.CssSelector("td"));

                currentName = columns[4].Text;
                if (!string.IsNullOrEmpty(previousName))
                {
                    Assert.IsTrue(StringComparer.Ordinal.Compare(previousName, currentName) <= 0);
                }
                previousName = currentName;
            }
        }
    }
}
