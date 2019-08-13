using OpenQA.Selenium;

namespace LitecartTesting.Forms
{
    public class LoginPage
    {
        private IWebDriver webDriver;

        public LoginPage(IWebDriver driver)
        {
            webDriver = driver;
        }

        public void Login(string username, string password)
        {
            webDriver.Url = "http://localhost/litecart/admin/";

            webDriver.FindElement(By.Name("username")).SendKeys(username);
            webDriver.FindElement(By.Name("password")).SendKeys(password);

            webDriver.FindElement(By.Name("login")).Click();
        }
    }
}
