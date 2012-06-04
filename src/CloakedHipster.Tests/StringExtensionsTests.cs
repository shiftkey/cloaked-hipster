using System;

namespace CloakedHipster.Tests
{
    public static class StringExtensionsTests
    {
        public static string ToTitleCase(this string s)
        {
            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TextInfo.ToTitleCase(s.ToLower());
        }

        public static string AsHexValue(this string s)
        {
            int result;
            if (int.TryParse(s, out result))
            {
                return Convert.ToString(result, 16).ToUpper();
            }
            return string.Empty;
        }
    }
}
