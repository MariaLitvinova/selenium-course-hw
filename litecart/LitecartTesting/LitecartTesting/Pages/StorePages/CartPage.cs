using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LitecartTesting.Pages.StorePages
{
    public class CartPage : PageBase
    {
        public IWebElement RemoveElement
            => webDriver.FindElement(By.Name("remove_cart_item"));

        public IWebElement Shortcuts
            => webDriver.FindElements(By.ClassName("shortcuts")).FirstOrDefault();

        public IWebElement OrderSummaryTable
            => webDriver.FindElement(By.CssSelector("#order_confirmation-wrapper .dataTable tbody"));

        public ReadOnlyCollection<IWebElement> OrderSummaryRows
            => OrderSummaryTable.FindElements(By.CssSelector("tr:not(.header)"));

        public CartPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public void RemoveAllItemsFromCart()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                By.CssSelector("#order_confirmation-wrapper .dataTable tbody")));

            while (true)
            {
                if (Shortcuts != null)
                {
                    var tabs = Shortcuts.FindElements(By.TagName("li"));
                    Assert.Greater(tabs.Count, 1);

                    var firstTabLink = tabs[0].FindElement(By.TagName("a"));
                    firstTabLink.Click();
                }

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("remove_cart_item")));
                var tableBeforeRemoving = OrderSummaryTable;

                RemoveElement.Click();

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(tableBeforeRemoving));

                var cartIsEmptyIndicator = webDriver.FindElements(By.CssSelector("#checkout-cart-wrapper p em"));
                if (cartIsEmptyIndicator.Count > 0 
                    && cartIsEmptyIndicator[0].Text.Contains("There are no items in your cart."))
                {
                    break;
                }
            }
        }
    }
}
