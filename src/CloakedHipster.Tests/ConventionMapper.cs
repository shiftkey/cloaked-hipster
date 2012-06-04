using System.Text.RegularExpressions;

namespace CloakedHipster.Tests
{
    public class ConventionMapper
    {
        Regex hexRegex = new Regex("#[0-9a-fA-F]{6}");

        public string MapKey(string key)
        {
            return key.ToTitleCase();
        }

        public string MapValue(string value)
        {
            if (hexRegex.IsMatch(value))
            {
                return value.ToUpper().Replace("#", "#FF");
            }

            return value.ToTitleCase();
        }
    }
}