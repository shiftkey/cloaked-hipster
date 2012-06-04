using System.Text;

namespace CloakedHipster.Tests
{
    public class CssProcessor
    {
        const string styleTemplate = "<Style x:Key=\"{0}\">{1}</Style>";
        const string setterTemplate = "<Setter Property=\"{0}\" Value=\"{1}\"/>";

        readonly CssParser process;
        readonly ConventionMapper mapper;

        public CssProcessor(CssParser process, ConventionMapper mapper)
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
                var setterBuilder = new StringBuilder();
                foreach (var v in style.Attributes)
                {
                    var mappedKey = mapper.MapKey(v.Key);
                    var mappedValue = mapper.MapValue(v.Value);

                    setterBuilder.AppendFormat(setterTemplate, mappedKey, mappedValue);
                }

                styleBuilder.AppendFormat(styleTemplate, style.Name, setterBuilder);
            }

            return styleBuilder.ToString();
        }
    }
}