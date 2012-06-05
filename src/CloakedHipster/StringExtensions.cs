using System;
using System.Threading;

namespace CloakedHipster
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string s)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TextInfo.ToTitleCase(s.ToLower());
        }

        public static string StripPixelValue(this string s)
        {
            return s.Replace("px", "");
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

        public static string MapDoubleToHexValue(this string input)
        {
            var opacityValue = Convert.ToDouble(input);
            var opacityIntValue = (int)(opacityValue * 256);
            var opacityHex = Convert.ToString(opacityIntValue, 16);

            if (opacityHex.Length == 1) // TODO: this should be more flexible
                opacityHex = "0" + opacityHex;

            return opacityHex;
        }
    }
}
