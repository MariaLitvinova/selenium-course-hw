﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitecartTesting.Helpers
{
    // TODO
    public static class SortingHelper
    {
        public static void CheckSorting(
            int amountOfItems,
            Func<int, IWebElement> getIthItem,
            Func<ReadOnlyCollection<IWebElement>, string> getSortingColumn,
            Action<ReadOnlyCollection<IWebElement>> callInnerSorting = null
            )
        {
            var previousName = "";
            for (int i = 0; i < amountOfItems; ++i)
            {
                var row = getIthItem(i);
                var columns = row.FindElements(By.CssSelector("td"));

                string currentName = getSortingColumn(columns);

                if (!string.IsNullOrEmpty(previousName) && !string.IsNullOrEmpty(currentName))
                {
                    Assert.IsTrue(StringComparer.Ordinal.Compare(previousName, currentName) <= 0);
                }

                callInnerSorting?.Invoke(columns);

                previousName = currentName;
            }
        }
    }
}
