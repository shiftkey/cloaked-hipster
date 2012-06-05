using System.Collections.Generic;
using System.Text;

namespace CloakedHipster
{
    public class CssProcessor
    {
        const string styleTemplate = "<Style x:Key=\"{0}\"{2}>{1}</Style>";
        const string setterTemplate = "    <Setter Property=\"{0}\" Value=\"{1}\"/>";

        readonly CssParser process;
        readonly IEnumerable<IMapper> mappers;

        public CssProcessor(CssParser process, IEnumerable<IMapper> mappers)
        {
            this.process = process;
            this.mappers = mappers;
        }

        public string Process(string input, Conventions conventions)
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

                    setterBuilder.AppendLine();
                    setterBuilder.AppendFormat(setterTemplate, result.Item1, result.Item2);
                }

                setterBuilder.AppendLine();
                var type = conventions.GetTargetType(style.Name);
                var targetType = "";
                if (!string.IsNullOrWhiteSpace(type))
                {
                    targetType = string.Format(" TargetType=\"{0}\"", type);
                }

                styleBuilder.AppendFormat(styleTemplate, style.Name, setterBuilder, targetType);
                styleBuilder.AppendLine();
            }

            return styleBuilder.ToString();
        }
    }
}