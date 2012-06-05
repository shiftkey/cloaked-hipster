using System.Collections.Generic;

namespace CloakedHipster
{
    public class Conventions
    {
        private IDictionary<string, string> conventions = new Dictionary<string, string>();

        public void Use(string input, string output)
        {
            conventions.Add(input, output);
        }

        public string GetTargetType(string name)
        {
            foreach (var convention in conventions)
            {
                if (name.Contains(convention.Key))
                {
                    return convention.Value;
                }
            }

            return string.Empty;
        }
    }
}