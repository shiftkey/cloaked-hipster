using System;

namespace CloakedHipster
{
    public interface IMapper
    {
        bool IsMatch(StyleClass styleClass);
        Tuple<string, string> Process(StyleClass styleClass);
    }
}