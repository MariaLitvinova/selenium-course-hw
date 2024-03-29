﻿using LitecartTesting.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LitecartTesting.Pages.AdministrationMenu
{
    public class InformationTab : PageBase
    {
        public IWebElement Manufacturer
            => webDriver.FindElement(By.Name("manufacturer_id"));

        public IWebElement Keywords
            => webDriver.FindElement(By.Name("keywords"));

        public IWebElement ShortDescription
            => webDriver.FindElement(By.Name("short_description[en]"));

        public IWebElement HeadTitle
            => webDriver.FindElement(By.Name("head_title[en]"));

        public IWebElement MetaDescription
            => webDriver.FindElement(By.Name("meta_description[en]"));

        public InformationTab(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public void FillInInformationTab(string productName)
        {
            wait.Until(driver => driver.FindElements(By.Name("manufacturer_id")).Count > 0);

            ExecuteScriptsHelper.SelectItemFromDropDown(Manufacturer, 1, webDriver);

            Keywords.SendKeys(RandomDataGenerator.RandomString(15));

            ShortDescription.SendKeys(RandomDataGenerator.RandomString(20));

            HeadTitle.SendKeys(productName);

            MetaDescription.SendKeys(RandomDataGenerator.RandomString());
        }
    }
}
