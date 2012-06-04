using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CloakedHipster.Tests
{
    public class BackgroundMapper : IMapper
    {
        Regex hexRegex = new Regex("#[0-9a-fA-F]{6}");
        Regex rgbRegex = new Regex(@"(^rgb\((\d+),\s*(\d+),\s*(\d+)\)$)|(^rgba\((\d+),\s*(\d+),\s*(\d+)(,\s*\d+\.\d+)*\)$)");
        Regex digitRegex = new Regex(@"[0-9]{1,3}");
        Regex digitWithDecimalRegex = new Regex(@"[0-9]{1}\.[0-9]{1,}");
        string BackgroundKey = "Background";

        public bool IsMatch(CssParser.StyleClass styleClass)
        {
            return styleClass.Attributes.Any(t => t.Key.StartsWith("background"));
        }

        public Tuple<string, string> Process(CssParser.StyleClass styleClass)
        {
            string value;
            if (styleClass.Attributes.TryGetValue("background-color", out value))
            {
                var result = rgbRegex.Match(value);
                if (result.Success)
                {
                    var numbers = digitRegex.Matches(value);

                    var first = numbers[0].Value;
                    var second = numbers[1].Value;
                    var third = numbers[2].Value;

                    var opacityRegex = digitWithDecimalRegex.Match(value);

                    if (opacityRegex.Success)
                    {
                        var opacity = opacityRegex.Value;
                        var opacityHex = AsHexValue(opacity);

                        var outputValue = string.Format("#{3}{0}{1}{2}", first.AsHexValue(), second.AsHexValue(), third.AsHexValue(), opacityHex);
                        return new Tuple<string, string>(BackgroundKey, outputValue);
                    }
                    else
                    {
                        var outputValue = string.Format("#FF{0}{1}{2}", first.AsHexValue(), second.AsHexValue(), third.AsHexValue());
                        return new Tuple<string, string>(BackgroundKey, outputValue);
                    }
                }
            }

            styleClass.Attributes.TryGetValue("background", out value);

            if (hexRegex.IsMatch(value))
            {
                var outputValue = value.ToUpper().Replace("#", "#FF");
                return new Tuple<string, string>(BackgroundKey, outputValue);
            }

            return new Tuple<string, string>(BackgroundKey, value.ToTitleCase());
        }

        private static string AsHexValue(string opacity, int length = 2)
        {
            var opacityValue = Convert.ToDouble(opacity);
            var opacityIntValue = (int)(opacityValue * 256);
            var opacityHex = Convert.ToString(opacityIntValue, 16);

            if (opacityHex.Length == 1)
                opacityHex = "0" + opacityHex;

            return opacityHex;
        }
    }
}