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

        public IWebElement SelectImage
            => webDriver.FindElement(By.Name("new_images[]"));

        public GeneralTab(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public string FillInGeneralTab()
        {
            EnabledOption.Click();

            var name = "Not a duck " + RandomDataGenerator.RandomString();
            Name.SendKeys(name);

            Code.SendKeys(RandomDataGenerator.RandomString());

            RubberDuckCategory.Click();
            UnisexGroup.Click();

            SelectImageForProduct();

            return name;
        }

        private void SelectImageForProduct()
        {
            Unhide(SelectImage);
            string path = TestContext.CurrentContext.TestDirectory;
            SelectImage.SendKeys(path + "/Resourses/pigeon.jpg");
        }

        private void Unhide(IWebElement element)
        {
            var script = "arguments[0].style.opacity=1;"
              + "arguments[0].style['transform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['MozTransform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['WebkitTransform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['msTransform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['OTransform']='translate(0px, 0px) scale(1)';"
              + "return true;";
            ((IJavaScriptExecutor)webDriver).ExecuteScript(script, element);
        }
    }
}
