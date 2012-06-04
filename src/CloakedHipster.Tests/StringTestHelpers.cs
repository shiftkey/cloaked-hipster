namespace CloakedHipster.Tests
{
    public static class StringTestHelpers
    {
        public static string IgnoreWhiteSpace(this string s)
        {
            return s.Replace("\r\n", "")
                .Replace("\r", "")
                .Replace("\n", "");
        }
    }
}