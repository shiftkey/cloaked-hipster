using System;

namespace CloakedHipster.Tests
{
    public interface IMapper
    {
        bool IsMatch(CssParser.StyleClass styleClass);
        Tuple<string, string> Process(CssParser.StyleClass styleClass);
    }
}