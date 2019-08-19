using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace LitecartTesting.Pages.AdministrationMenu
{
    public class CountriesPage : PageBase
    {
        public IWebElement CountriesTable => webDriver.FindElement(By.ClassName("dataTable"));

        public IWebElement GeozonesTable => webDriver.FindElement(By.Id("table-zones"));

        public ReadOnlyCollection<IWebElement> CountriesList => CountriesTable.FindElements(By.CssSelector("tr .row"));

        public ReadOnlyCollection<IWebElement> GeozonesList => GeozonesTable.FindElements(By.CssSelector("tr:not(.header)"));

        public IWebElement CancelEditingGeozonesButton => webDriver.FindElement(By.Name("cancel"));

        public CountriesPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public void CheckSorting()
        {
            WaitUntilHeaderAppears("Countries");

            var previousName = "";
            for (int i = 0; i < CountriesList.Count; ++i)
            {
                var row = CountriesList[i];
                var columns = row.FindElements(By.CssSelector("td"));

                string currentName = columns[4].Text;
                var currentAmountOfGeozones = columns[5].Text;

                if (!string.IsNullOrEmpty(previousName))
                {
                    Assert.IsTrue(StringComparer.Ordinal.Compare(previousName, currentName) <= 0);
                }

                if (int.Parse(currentAmountOfGeozones) > 0)
                {
                    var href = row.FindElement(By.CssSelector("a"));
                    href.Click();
                    CheckSortingOfGeozonesOnCountryPage();
                }

                previousName = currentName;
            }
        }

        private void CheckSortingOfGeozonesOnCountryPage()
        {
            WaitUntilHeaderAppears("Edit Country");

            var previousName = "";
            for (int i = 0; i < GeozonesList.Count; ++i)
            {
                var row = GeozonesList[i];
                var columns = row.FindElements(By.CssSelector("td"));

                string currentName = columns[2].Text;
                if (!string.IsNullOrEmpty(previousName) && !string.IsNullOrEmpty(currentName))
                {
                    Assert.IsTrue(StringComparer.Ordinal.Compare(previousName, currentName) <= 0);
                }

                previousName = currentName;
            }

            CancelEditingGeozonesButton.Click();

            WaitUntilHeaderAppears("Countries");
        }
    }
}
