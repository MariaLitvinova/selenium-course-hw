using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Linq;

namespace LitecartTesting.Pages.AdministrationMenu
{
    public class CatalogPage : PageBase
    {
        public IWebElement CreateNewProductButton
            => webDriver.FindElement(By.LinkText("Add New Product"));

        public IWebElement Table
            => webDriver.FindElement(By.ClassName("dataTable"));

        public ReadOnlyCollection<IWebElement> Rows
            => Table.FindElements(By.CssSelector("tr.row"));

        public CatalogPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public string CreateNewProduct()
        {
            WaitUntilHeaderAppears("Catalog");

            CreateNewProductButton.Click();

            var createNewProductPage = new CreateNewProductPage(webDriver, wait);
            var createdProductName = createNewProductPage.CreateNewProduct();

            var products = Table.FindElements(By.CssSelector("tr.row"));

            var productNames = products
                .Select(x =>
                {
                    var columns = x.FindElements(By.CssSelector("td"));
                    return columns[2].Text;
                })
                .ToList();

            Assert.Contains(createdProductName, productNames);

            return createdProductName;
        }

        public void ClickOnAllProductsAndCheckLogs()
        {
            WaitUntilHeaderAppears("Catalog");

            OpenAllFolders();

            ClickOnAllProducts();
        }

        private void OpenAllFolders()
        {
            for (int i = 0; i < Rows.Count; ++i)
            {
                var row = Rows[i];
                var category = row.FindElements(By.CssSelector("td"))[2];
                var folderElements = category.FindElements(By.CssSelector("i.fa-folder"));
                if (folderElements.Any())
                {
                    var href = category.FindElement(By.CssSelector("a"));
                    href.Click();
                    OpenAllFolders();
                }
            }
        }

        private void ClickOnAllProducts()
        {
            for (int i = 0; i < Rows.Count; ++i)
            {
                var row = Rows[i];
                var category = row.FindElements(By.CssSelector("td"))[2];
                var duckPicture = category.FindElements(By.CssSelector("img"));
                if (duckPicture.Any())
                {
                    var href = category.FindElement(By.CssSelector("a"));
                    href.Click();

                    var editProductPage = new EditProductPage(webDriver, wait);
                    editProductPage.CheckPageLoaded();

                    // падает с ошибкой System.NotImplementedException : unknown command: unknown command: session/e112d8e0de3bda850127f3cd6bab27cd/se/logs
                    // нашла такое ишью https://github.com/SeleniumHQ/selenium/issues/7390
                    // и фикс https://github.com/SeleniumHQ/selenium/commit/26d8b67a58e99fa4121376537ea434c761eea5e6#diff-5fedfeb3060c9b589bc5ac90334761ca
                    // написано, что он будет в 4.0 alpha 3
                    // что можно с этим сделать?

                    //var logs = webDriver.Manage().Logs.GetLog(LogType.Browser);
                    //Assert.AreEqual(0, logs.Count);

                    editProductPage.CancelButton.Click();
                    WaitUntilHeaderAppears("Catalog");
                }
            }
        }
    }
}
