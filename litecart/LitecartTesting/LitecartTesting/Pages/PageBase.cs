using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LitecartTesting.Pages
{
    public class PageBase
    {
        protected readonly IWebDriver webDriver;
        protected readonly WebDriverWait wait;

        public PageBase(IWebDriver driver, WebDriverWait wait)
        {
            webDriver = driver;
            this.wait = wait;
        }

        protected void WaitUntilHeaderAppears(string header)
        {
            bool headerAppeared = wait.Until(driver =>
                driver.FindElements(By.CssSelector("td#content h1")).Count == 1
                && driver.FindElement(By.CssSelector("td#content h1")).Text == header);
            Assert.IsTrue(headerAppeared);
        }
    }
}
