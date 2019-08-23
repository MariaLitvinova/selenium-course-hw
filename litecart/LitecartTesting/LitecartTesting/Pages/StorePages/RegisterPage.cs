using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitecartTesting.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LitecartTesting.Pages.StorePages
{
    public class RegisterPage : PageBase
    {
        public IWebElement FirstName => webDriver.FindElement(By.Name("firstname"));

        public IWebElement LastName => webDriver.FindElement(By.Name("lastname"));

        public IWebElement Address1 => webDriver.FindElement(By.Name("address1"));

        public IWebElement Postcode => webDriver.FindElement(By.Name("postcode"));

        public IWebElement City => webDriver.FindElement(By.Name("city"));

        public IWebElement Email => webDriver.FindElement(By.Name("email"));

        public IWebElement Phone => webDriver.FindElement(By.Name("phone"));

        public IWebElement Password => webDriver.FindElement(By.Name("password"));

        public IWebElement ConfirmedPassword => webDriver.FindElement(By.Name("confirmed_password"));

        public IWebElement SubmitButton => webDriver.FindElement(By.Name("create_account"));

        public RegisterPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public (string email, string password) CreateAccount()
        {
            FirstName.SendKeys("Name_" + RandomDataGenerator.RandomString());

            LastName.SendKeys("Surname_" + RandomDataGenerator.RandomString());

            Address1.SendKeys("Address_" + RandomDataGenerator.RandomString());

            Postcode.SendKeys(RandomDataGenerator.RandomStringOfNumbers(6));

            City.SendKeys("City_" + RandomDataGenerator.RandomString());

            var email = "email_" + RandomDataGenerator.RandomString(5) + "@gmail.com";

            Email.SendKeys(email);

            Phone.SendKeys("+7" + RandomDataGenerator.RandomStringOfNumbers(10));

            var password = RandomDataGenerator.RandomString(15);

            Password.SendKeys(password);
            ConfirmedPassword.SendKeys(password);

            SubmitButton.Click();

            return (email, password);
        }
    }
}
