using System;
using System.Linq;

namespace LitecartTesting.Helpers
{
    public static class RandomDataGenerator
    {
        private static readonly Random random = new Random();

        public static string RandomString(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return RandomSequence(chars, length);
        }

        public static string RandomStringOfNumbers(int length = 10)
        {
            const string chars = "0123456789";
            return RandomSequence(chars, length);
        }

        private static string RandomSequence(string chars, int length)
        {
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
