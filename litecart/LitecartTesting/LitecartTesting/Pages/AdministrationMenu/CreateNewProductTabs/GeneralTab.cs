using LitecartTesting.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace LitecartTesting.Pages.AdministrationMenu
{
    public class GeneralTab : PageBase
    {
        public IWebElement EnabledOption
            => webDriver.FindElement(By.CssSelector("input[type=radio]"));

        public IWebElement Name
            => webDriver.FindElement(By.Name("name[en]"));

        public IWebElement Code
            => webDriver.FindElement(By.Name("code"));

        public IWebElement RubberDuckCategory
            => webDriver.FindElement(By.CssSelector("#tab-general > table > tbody > tr:nth-child(4) > td > div > table > tbody > tr:nth-child(2) > td:nth-child(1) > input[type=checkbox]"));

        public IWebElement UnisexGroup
            => webDriver.FindElement(By.CssSelector("#tab-general > table > tbody > tr:nth-child(7) > td > div > table > tbody > tr:nth-child(4) > td:nth-child(1) > input[type=checkbox]"));

        public IWebElement Quantity
            => webDriver.FindElement(By.Name("quantity"));

        public IWebElement SoldOutStatus
            => webDriver.FindElement(By.Name("sold_out_status_id"));

        public IWebElement SelectImage
            => webDriver.FindElement(By.Name("new_images[]"));

        public IWebElement ValidFrom
            => webDriver.FindElement(By.Name("date_valid_from"));

        public GeneralTab(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public string FillInGeneralTab()
        {
            EnabledOption.Click();

            var name = "Not a duck " + RandomDataGenerator.RandomString();
            Name.SendKeys(name);

            Code.SendKeys(RandomDataGenerator.RandomString());

            RubberDuckCategory.Click();
            UnisexGroup.Click();

            Quantity.SendKeys(Keys.Control + "a" + Keys.Backspace);
            Quantity.SendKeys("10");

            ExecuteScriptsHelper.SelectItemFromDropDown(SoldOutStatus, 0, webDriver);

            SelectImageForProduct();

            ValidFrom.SendKeys(DateTime.Now.ToString("ddMMyyyy"));

            return name;
        }

        private void SelectImageForProduct()
        {
            ExecuteScriptsHelper.Unhide(SelectImage, webDriver);
            string path = TestContext.CurrentContext.TestDirectory;
            SelectImage.SendKeys(path + "/Resourses/pigeon.jpg");
        }
    }
}
