using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitecartTesting.Helpers
{
    public static class ExecuteScriptsHelper
    {
        public static void Unhide(IWebElement element, IWebDriver webDriver)
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

        public static void SelectItemFromDropDown(IWebElement element, int index, IWebDriver webDriver)
        {
            var script = $"arguments[0].selectedIndex={index}; arguments[0].dispatchEvent(new Event('change'))";
            ((IJavaScriptExecutor)webDriver).ExecuteScript(script, element);
        }
    }
}
