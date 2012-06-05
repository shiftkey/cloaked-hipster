using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CloakedHipster
{
    public static class Transformer
    {
        public static string Generate(string input, Conventions conventions)
        {
            var mappers = new List<IMapper>
                              {
                                  new BackgroundMapper(), 
                                  new FontSizeMapper(), 
                                  new FontFamilyMapper(), 
                                  new MarginMapper()
                              };

            var processor = new CssProcessor(new CssParser(), mappers);
            return processor.Process(input, conventions);
        }
    }

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
