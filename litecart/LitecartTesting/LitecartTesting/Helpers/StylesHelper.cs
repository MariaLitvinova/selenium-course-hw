using NUnit.Framework;
using OpenQA.Selenium;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LitecartTesting.Helpers
{
    public static class StylesHelper
    {
        /// <summary>
        /// Проверка стилей продукта
        /// </summary>
        /// <param name="regularPrice"><Обычная цена/param>
        /// <param name="campaignPrice">Акционная цена</param>
        public static void CheckProductStyles(
            IWebElement regularPrice,
            IWebElement campaignPrice,
            string webDriverName)
        {
            var regularPriceColor = regularPrice.GetCssValue("color");
            var campaignPriceColor = campaignPrice.GetCssValue("color");

            var regularColorMatched = ParseColorValue(regularPriceColor, webDriverName);
            Assert.AreEqual(regularColorMatched.r, regularColorMatched.g);
            Assert.AreEqual(regularColorMatched.g, regularColorMatched.b);

            var campaignColorMatched = ParseColorValue(campaignPriceColor, webDriverName);
            Assert.AreEqual(0, campaignColorMatched.g);
            Assert.AreEqual(0, campaignColorMatched.b);

            string regularPriceDecoration;
            if (webDriverName == "EdgeDriver")
            {
                regularPriceDecoration = regularPrice.GetCssValue("text-decoration");
            } else
            {
                regularPriceDecoration = regularPrice.GetCssValue("text-decoration-line");
            }

            Assert.AreEqual("line-through", regularPriceDecoration);

            var campaignPriceFontWeight = int.Parse(campaignPrice.GetCssValue("font-weight"));
            Assert.Greater(campaignPriceFontWeight, 400);

            var regularPriceSize = regularPrice.GetCssValue("font-size");
            var regularPriceSizeParsed = ParseFontSize(regularPriceSize);
            var campaignPriceSize = campaignPrice.GetCssValue("font-size");
            var campaignPriceSizeParsed = ParseFontSize(campaignPriceSize);
            Assert.Greater(campaignPriceSizeParsed, regularPriceSizeParsed);
        }

        private static (int r, int g, int b) ParseColorValue(string color, string webDriverName)
        {
            Regex regex;

            if (webDriverName == "ChromeDriver")
            {
                regex = new Regex(@"rgba\((?<r>\d+), (?<g>\d+), (?<b>\d+), (?<a>\d+)\)");
            } else
            {
                regex = new Regex(@"rgb\((?<r>\d+), (?<g>\d+), (?<b>\d+)\)");
            }

            var match = regex.Match(color);

            var r = int.Parse(match.Groups["r"].Value);
            var g = int.Parse(match.Groups["g"].Value);
            var b = int.Parse(match.Groups["b"].Value);

            return (r, g, b);
        }

        private static double ParseFontSize(string stringRepresentation)
        {
            var size = Regex.Match(stringRepresentation, @"\d+(?:\.\d+)?").Value;
            return double.Parse(size, CultureInfo.InvariantCulture);
        }
    }
}
