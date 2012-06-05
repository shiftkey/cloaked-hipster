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
}
