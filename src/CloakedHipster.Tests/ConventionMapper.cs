namespace CloakedHipster.Tests
{
    public class ConventionMapper
    {
        public string MapKey(string key)
        {
            return key.ToTitleCase();
        }

        public string MapValue(string value)
        {
            return value.ToTitleCase();
        }
    }
}