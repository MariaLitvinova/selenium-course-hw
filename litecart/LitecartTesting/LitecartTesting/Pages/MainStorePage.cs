using LitecartTesting.Helpers;
using LitecartTesting.Pages.StorePages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Linq;

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

        public IWebElement HomeButton
            => webDriver.FindElement(By.CssSelector("li.general-0 a"));

        public IWebElement LoginForm
            => webDriver.FindElement(By.CssSelector("#box-account-login"));

        public IWebElement EmailTextArea
            => LoginForm.FindElement(By.Name("email"));

        public IWebElement PasswordTextArea
            => LoginForm.FindElement(By.Name("password"));
        public IWebElement LoginButton
            => LoginForm.FindElement(By.Name("login"));

        public IWebElement RegisterLink
            => LoginForm.FindElement(By.TagName("a"));

        public IWebElement AccountForm
            => webDriver.FindElement(By.CssSelector("#box-account"));

        public IWebElement CustomerService
            => AccountForm.FindElements(By.CssSelector("li"))[0].FindElement(By.CssSelector("a"));

        public IWebElement OrderHistory
            => AccountForm.FindElements(By.CssSelector("li"))[1].FindElement(By.CssSelector("a"));

        public IWebElement EditAccount
            => AccountForm.FindElements(By.CssSelector("li"))[2].FindElement(By.CssSelector("a"));

        public IWebElement Logout
            => AccountForm.FindElements(By.CssSelector("li"))[3].FindElement(By.CssSelector("a"));

        public IWebElement CartWrapper
            => webDriver.FindElement(By.Id("cart"));

        public IWebElement Checkout
            => CartWrapper.FindElement(By.PartialLinkText("Checkout"));

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

        public void BuyFirstDuck()
        {
            var currentQuantity = int.Parse(CartWrapper.FindElement(By.CssSelector(".content .quantity")).Text);

            var duckToBuy = DucksList.FirstOrDefault();
            Assert.IsNotNull(duckToBuy);

            var link = duckToBuy.FindElement(By.CssSelector("a.link"));
            link.Click();

            var productPage = new ProductPage(webDriver, wait);
            productPage.CheckPageIsLoaded();
            productPage.BuyProduct();

            wait.Until(driver => 
                CartWrapper.FindElements(By.CssSelector(".content .quantity")).Count > 0
                && int.Parse(CartWrapper.FindElement(By.CssSelector(".content .quantity")).Text) == currentQuantity + 1);

            HomeButton.Click();
        }

        public void CheckYellowDuckStyle(string webDriverName)
        {
            var campaignSection = webDriver.FindElement(By.CssSelector("#box-campaigns"));
            var yellowDuck = campaignSection.FindElements(By.CssSelector(".product")).FirstOrDefault();
            Assert.IsNotNull(yellowDuck);

            var productName = yellowDuck.FindElement(By.CssSelector(".name"));
            var regularPrice = yellowDuck.FindElements(By.CssSelector(".regular-price")).FirstOrDefault();
            var campaignPrice = yellowDuck.FindElements(By.CssSelector(".campaign-price")).FirstOrDefault();

            StylesHelper.CheckProductStyles(regularPrice, campaignPrice, webDriverName);

            var productNameText = productName.Text;
            var regularPriceText = regularPrice.Text;
            var campaignPriceText = campaignPrice?.Text;

            var link = yellowDuck.FindElement(By.CssSelector("a.link"));
            link.Click();

            var productPage = new ProductPage(webDriver, wait);
            productPage.CheckPageIsLoaded();

            productPage.CheckStylesForProduct(productNameText, regularPriceText, campaignPriceText, webDriverName);

            HomeButton.Click();
        }

        public void Login(string email, string password)
        {
            wait.Until(driver => driver.Title.Contains("Online Store"));

            EmailTextArea.SendKeys(email);
            PasswordTextArea.SendKeys(password);
            LoginButton.Click();
        }
    }
}
