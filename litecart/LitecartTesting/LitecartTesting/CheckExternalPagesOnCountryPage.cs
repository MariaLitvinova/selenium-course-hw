﻿using LitecartTesting.Forms;
using LitecartTesting.Pages.AdministrationMenu;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace LitecartTesting
{
    [TestFixture]
    public class CheckExternalPagesOnCountryPage
    {
        private IWebDriver webDriver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            webDriver = new ChromeDriver();
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(30));
        }

        [Test]
        public void CheckExternalPagesOnCountryPageTest()
        {
            var loginPage = new LoginPage(webDriver);
            loginPage.Login("admin", "admin");

            var administrationPage = new AdministrationPage(webDriver, wait);
            administrationPage.CountriesMenu.Click();

            var countriesPage = new CountriesPage(webDriver, wait);
            countriesPage.CreateNewCountryButton.Click();

            var createNewCountryTab = new CreateNewCountryTab(webDriver, wait);
            createNewCountryTab.ClickOnAllExternalLinks();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
            webDriver = null;
        }
    }
}
