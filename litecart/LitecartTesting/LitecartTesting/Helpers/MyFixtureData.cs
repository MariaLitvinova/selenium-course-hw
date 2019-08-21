using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections;

namespace LitecartTesting.Helpers
{
    public class MyFixtureData
    {
        public static IEnumerable FixtureParameters
        {
            get
            {
                yield return new TestFixtureData(new Func<IWebDriver>(() => new ChromeDriver()));
                yield return new TestFixtureData(new Func<IWebDriver>(() => new EdgeDriver()));
                yield return new TestFixtureData(new Func<IWebDriver>(() => new FirefoxDriver(new FirefoxOptions() { BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe" })));
            }
        }
    }
}
