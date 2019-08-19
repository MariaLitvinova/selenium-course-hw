using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LitecartTesting.Pages.AdministrationMenu
{
    public class GeozonesPage : PageBase
    {
        public IWebElement GeozonesTable => webDriver.FindElement(By.ClassName("dataTable"));

        public IWebElement ZonesTable => webDriver.FindElement(By.Id("table-zones"));

        public ReadOnlyCollection<IWebElement> GeozonesList => GeozonesTable.FindElements(By.CssSelector("tr.row"));

        public ReadOnlyCollection<IWebElement> ZonesList => ZonesTable.FindElements(By.CssSelector("tr:not(.header)"));

        public IWebElement CancelEditingGeozoneButton => webDriver.FindElement(By.Name("cancel"));

        public GeozonesPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public void CheckSorting()
        {
            WaitUntilHeaderAppears("Geo Zones");

            var previousName = "";
            for (int i = 0; i < GeozonesList.Count; ++i)
            {
                var row = GeozonesList[i];
                var columns = row.FindElements(By.CssSelector("td"));

                string currentName = columns[2].Text;
                var currentAmountOfZones = columns[3].Text;

                if (!string.IsNullOrEmpty(previousName))
                {
                    Assert.IsTrue(StringComparer.Ordinal.Compare(previousName, currentName) <= 0);
                }

                if (int.Parse(currentAmountOfZones) > 0)
                {
                    var href = row.FindElement(By.CssSelector("a"));
                    href.Click();
                    CheckSortingOfZonesOnGeozonePage();
                }

                previousName = currentName;
            }
        }

        private void CheckSortingOfZonesOnGeozonePage()
        {
            WaitUntilHeaderAppears("Edit Geo Zone");

            var previousName = "";
            for (int i = 0; i < ZonesList.Count; ++i)
            {
                var row = ZonesList[i];
                var columns = row.FindElements(By.CssSelector("td"));
                if (columns.Count < 3)
                {
                    break;
                }

                string currentName = columns[2]
                    .FindElements(By.CssSelector("option"))
                    .FirstOrDefault(x => x.GetAttribute("selected") == "true")
                    .Text;
                if (!string.IsNullOrEmpty(previousName) && !string.IsNullOrEmpty(currentName))
                {
                    Assert.IsTrue(StringComparer.Ordinal.Compare(previousName, currentName) <= 0);
                }

                previousName = currentName;
            }

            CancelEditingGeozoneButton.Click();

            WaitUntilHeaderAppears("Geo Zones");
        }
    }
}
