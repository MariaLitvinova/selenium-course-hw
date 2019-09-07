using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LitecartTesting.Pages.AdministrationMenu
{
    public class EditProductPage : PageBase
    {
        public IWebElement CancelButton
            => webDriver.FindElement(By.Name("cancel"));

        public EditProductPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public void CheckPageLoaded()
        {
            WaitUntilHeaderAppears("Edit Product");
        }
    }
}
