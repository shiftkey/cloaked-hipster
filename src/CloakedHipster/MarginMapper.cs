using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CloakedHipster
{
    public class MarginMapper : IMapper
    {
        public bool IsMatch(StyleClass styleClass)
        {
            return styleClass.Attributes.Any(c => c.Key.StartsWith("margin"));
        }

        public Tuple<string, string> Process(StyleClass styleClass)
        {
            var template = "{0},{1},{2},{3}";
            var topValue = "0";
            var leftValue = "0";
            var rightValue = "0";
            var bottomValue = "0";

            string value;

            if (styleClass.Attributes.TryGetValue("margin", out value))
            {
                var providedValues = new Regex("[0-9]{1,}px");
                var results = providedValues.Matches(value);

                if (results.Count == 4)
                {
                    topValue = results[0].Value.StripPixelValue();
                    rightValue = results[1].Value.StripPixelValue();
                    bottomValue = results[2].Value.StripPixelValue();
                    leftValue = results[3].Value.StripPixelValue();
                }

                if (results.Count == 3)
                {
                    var first = results[0].Value.StripPixelValue();
                    var second = results[1].Value.StripPixelValue();
                    var third = results[2].Value.StripPixelValue();
                    topValue = first;
                    rightValue = second;
                    bottomValue = third;
                    leftValue = second;
                }

                if (results.Count == 2)
                {
                    var first = results[0].Value.StripPixelValue();
                    var second = results[1].Value.StripPixelValue();
                    topValue = first;
                    rightValue = second;
                    bottomValue = first;
                    leftValue = second;
                }

                if (results.Count == 1)
                {
                    var first = results[0].Value.StripPixelValue();
                    topValue = first;
                    rightValue = first;
                    bottomValue = first;
                    leftValue = first;
                }
            }

            if (styleClass.Attributes.TryGetValue("margin-left", out value))
            {
                leftValue = value.StripPixelValue();
            }

            if (styleClass.Attributes.TryGetValue("margin-top", out value))
            {
                topValue = value.StripPixelValue();
            }

            if (styleClass.Attributes.TryGetValue("margin-bottom", out value))
            {
                bottomValue = value.StripPixelValue();
            }

            if (styleClass.Attributes.TryGetValue("margin-right", out value))
            {
                rightValue = value.StripPixelValue();
            }

            var result = string.Format(template, leftValue, topValue, rightValue, bottomValue);
            return new Tuple<string, string>("Margin", result);
        }
    }
}