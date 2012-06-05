using System;

namespace CloakedHipster
{
    public interface IMapper
    {
        bool IsMatch(CssParser.StyleClass styleClass);
        Tuple<string, string> Process(CssParser.StyleClass styleClass);
    }
}