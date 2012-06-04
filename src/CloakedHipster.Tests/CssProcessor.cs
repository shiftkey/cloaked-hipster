using System.Text;

namespace CloakedHipster.Tests
{
    public class CssProcessor
    {
        const string styleTemplate = "<Style x:Key=\"{0}\">{1}</Style>";
        const string setterTemplate = "    <Setter Property=\"{0}\" Value=\"{1}\"/>";

        readonly CssParser process;
        readonly BackgroundMapper mapper;

        public CssProcessor(CssParser process, BackgroundMapper mapper)
        {
            this.process = process;
            this.mapper = mapper;
        }

        public string Process(string input)
        {
            var styles = process.Parse(input);
            var styleBuilder = new StringBuilder();

            foreach (var style in styles)
            {
                if (!mapper.IsMatch(style))
                    continue;

                var setterBuilder = new StringBuilder();

                var result = mapper.Process(style);

                if (result == null)
                    continue;

                setterBuilder.AppendFormat(setterTemplate, result.Item1, result.Item2);

                styleBuilder.AppendFormat(styleTemplate, style.Name, setterBuilder);
            }

            return styleBuilder.ToString();
        }
    }
}