using System.Collections.Generic;
using System.Text;

namespace CloakedHipster.Tests
{
    public class CssProcessor
    {
        const string styleTemplate = "<Style x:Key=\"{0}\">{1}</Style>";
        const string setterTemplate = "    <Setter Property=\"{0}\" Value=\"{1}\"/>";

        readonly CssParser process;
        readonly IEnumerable<IMapper> mappers;

        public CssProcessor(CssParser process, IEnumerable<IMapper> mappers)
        {
            this.process = process;
            this.mappers = mappers;
        }

        public string Process(string input)
        {
            var styles = process.Parse(input);
            var styleBuilder = new StringBuilder();

            foreach (var style in styles)
            {
                var setterBuilder = new StringBuilder();

                foreach (var mapper in mappers)
                {
                    if (!mapper.IsMatch(style))
                        continue;

                    var result = mapper.Process(style);

                    if (result == null)
                        continue;

                    setterBuilder.AppendFormat(setterTemplate, result.Item1, result.Item2);
                }

                styleBuilder.AppendFormat(styleTemplate, style.Name, setterBuilder);
            }

            return styleBuilder.ToString();
        }
    }
}