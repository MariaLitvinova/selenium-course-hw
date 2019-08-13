using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LitecartTesting.Forms
{
    public class AdministrationPage
    {
        private readonly IWebDriver webDriver;
        private readonly WebDriverWait wait;

        private readonly List<MenuItem> expectedMenuStructure = new List<MenuItem>()
        {
            new MenuItem("Appearence", "Template", new List<string>()
                {
                    "Template",
                    "Logotype",
                }),
            new MenuItem("Catalog", "Catalog", new List<string>()
                {
                    "Catalog",
                    "Product Groups",
                    "Option Groups",
                    "Manufacturers",
                    "Suppliers",
                    "Delivery Statuses",
                    "Sold Out Statuses",
                    "Quantity Units",
                    "CSV Import/Export"
                }),
            new MenuItem("Countries"),
            new MenuItem("Currencies"),
            new MenuItem("Customers", "Customers", new List<string>()
                {
                    "Customers",
                    "CSV Import/Export",
                    "Newsletter"
                }),
            new MenuItem("Geo Zones"),
            new MenuItem("Languages", "Languages", new List<string>()
                {
                    "Languages",
                    "Storage Encoding"
                }),
            new MenuItem("Modules", "Job Modules", new List<(string, string)>()
                {
                    ("Background Jobs", "Job Modules"),
                    ("Customer", "Customer Modules"),
                    ("Shipping", "Shipping Modules"),
                    ("Payment", "Payment Modules"),
                    ("Order Total", "Order Total Modules"),
                    ("Order Success", "Order Success Modules"),
                    ("Order Action", "Order Action Modules")
                }),
            new MenuItem("Orders", "Orders", new List<string>()
                {
                    "Orders",
                    "Order Statuses"
                }),
            new MenuItem("Pages"),
            new MenuItem("Reports", "Monthly Sales", new List<string>()
                {
                    "Monthly Sales",
                    "Most Sold Products",
                    "Most Shopping Customers"
                }),
            new MenuItem("Settings", "Settings", new List<(string, string)>()
                {
                    ("Store Info", "Settings"),
                    ("Defaults", "Settings"),
                    ("General", "Settings"),
                    ("Listings", "Settings"),
                    ("Images", "Settings"),
                    ("Checkout", "Settings"),
                    ("Advanced", "Settings"),
                    ("Security", "Settings")
                }),
            new MenuItem("Slides"),
            new MenuItem("Tax", "Tax Classes", new List<string>()
                {
                    "Tax Classes",
                    "Tax Rates"
                }),
            new MenuItem("Translations", "Search Translations", new List<(string, string)>()
                {
                    ("Search Translations", "Search Translations"),
                    ("Scan Files", "Scan Files For Translations"),
                    ("CSV Import/Export", "CSV Import/Export")
                }),
            new MenuItem("Users"),
            new MenuItem("vQmods", "vQmods", new List<string>()
                {
                    "vQmods"
                })
        };

        private class MenuItem
        {
            public string ItemText { get; set; }
            public string HeaderText { get; set; }
            public List<MenuItem> SubMenuItems { get; set; }

            public MenuItem(string itemText)
            {
                ItemText = itemText;
                HeaderText = itemText;
                SubMenuItems = null;
            }

            public MenuItem(string itemText, string headerText, List<string> subMenuItems)
            {
                ItemText = itemText;
                HeaderText = headerText;
                SubMenuItems = subMenuItems
                    .Select(x => new MenuItem(x))
                    .ToList();
            }

            public MenuItem(string itemText, string headerText)
            {
                ItemText = itemText;
                HeaderText = headerText;
                SubMenuItems = null;
            }

            public MenuItem(
                string itemText,
                string headerText,
                List<(string submenuItemText, string subMenuItemHeader)> subMenuItems)
            {
                ItemText = itemText;
                HeaderText = headerText;
                SubMenuItems = subMenuItems
                    .Select(x => new MenuItem(x.submenuItemText, x.subMenuItemHeader))
                    .ToList();
            }
        }

        public ReadOnlyCollection<IWebElement> MainMenuItems => webDriver.FindElements(By.CssSelector("div#box-apps-menu-wrapper #app-"));

        public ReadOnlyCollection<IWebElement> SubMenuItems => webDriver.FindElements(By.CssSelector("div#box-apps-menu-wrapper #app- .docs li"));

        public AdministrationPage(IWebDriver driver, WebDriverWait wait)
        {
            webDriver = driver;
            this.wait = wait;
        }

        public void ClickOnAllMenuItems()
        {
            var pageLoaded = wait.Until(driver => driver.FindElements(By.CssSelector("#box-apps-menu-wrapper")).Count == 1);
            Assert.IsTrue(pageLoaded);
            Assert.AreEqual(expectedMenuStructure.Count, MainMenuItems.Count);

            for (int i = 0; i < MainMenuItems.Count; ++i)
            {
                var expectedMenuItem = expectedMenuStructure[i];
                var actualMenuItem = MainMenuItems[i];

                Assert.AreEqual(expectedMenuItem.ItemText, actualMenuItem.Text);

                actualMenuItem.Click();
                
                bool correctHeaderAppeared = wait.Until(driver =>
                    driver.FindElements(By.CssSelector("td#content h1")).Count == 1
                    && driver.FindElement(By.CssSelector("td#content h1")).Text == expectedMenuItem.HeaderText);

                Assert.IsTrue(correctHeaderAppeared);
                var expectedNumberOfSubmenuItems = expectedMenuItem.SubMenuItems?.Count ?? 0;
                Assert.AreEqual(expectedNumberOfSubmenuItems, SubMenuItems.Count);
                
                for (int j = 0; j < SubMenuItems.Count; ++j)
                {
                    var jthSubmenuItem = SubMenuItems[j];
                    Assert.AreEqual(expectedMenuItem.SubMenuItems[j].ItemText, jthSubmenuItem.Text);

                    jthSubmenuItem.Click();
                    bool headerAppeared = wait.Until(driver =>
                        driver.FindElements(By.CssSelector("td#content h1")).Count == 1
                        && driver.FindElement(By.CssSelector("td#content h1")).Text == expectedMenuItem.SubMenuItems[j].HeaderText);
                    Assert.IsTrue(headerAppeared);
                }
            }
        }
    }
}
