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
            IWebElement campaignPrice)
        {
            var regularPriceColor = regularPrice.GetCssValue("color");
            var campaignPriceColor = campaignPrice.GetCssValue("color");

            var regularColorMatched = ParseColorValue(regularPriceColor);
            Assert.AreEqual(regularColorMatched.r, regularColorMatched.g);
            Assert.AreEqual(regularColorMatched.g, regularColorMatched.b);

            var campaignColorMatched = ParseColorValue(campaignPriceColor);
            Assert.AreEqual(0, campaignColorMatched.g);
            Assert.AreEqual(0, campaignColorMatched.b);

            var regularPriceDecoration = regularPrice.GetCssValue("text-decoration-line");
            Assert.AreEqual("line-through", regularPriceDecoration);

            var campaignPriceFontWeight = campaignPrice.GetCssValue("font-weight");
            Assert.AreEqual("700", campaignPriceFontWeight);

            var regularPriceSize = regularPrice.GetCssValue("font-size");
            var regularPriceSizeParsed = ParseFontSize(regularPriceSize);
            var campaignPriceSize = campaignPrice.GetCssValue("font-size");
            var campaignPriceSizeParsed = ParseFontSize(campaignPriceSize);
            Assert.Greater(campaignPriceSizeParsed, regularPriceSizeParsed);
        }

        private static (int r, int g, int b, int a) ParseColorValue(string color)
        {
            var regex = new Regex(@"rgba\((?<r>\d+), (?<g>\d+), (?<b>\d+), (?<a>\d+)\)");
            var match = regex.Match(color);

            var r = int.Parse(match.Groups["r"].Value);
            var g = int.Parse(match.Groups["g"].Value);
            var b = int.Parse(match.Groups["b"].Value);
            var a = int.Parse(match.Groups["a"].Value);

            return (r, g, b, a);
        }

        private static double ParseFontSize(string stringRepresentation)
        {
            var size = Regex.Match(stringRepresentation, @"\d+(?:\.\d+)?").Value;
            return double.Parse(size, CultureInfo.InvariantCulture);
        }
    }
}
