namespace CloakedHipster.Tests
{
    public static class StringExtensionsTests
    {
        public static string ToTitleCase(this string s)
        {
            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TextInfo.ToTitleCase(s.ToLower());
        }
    }
}
