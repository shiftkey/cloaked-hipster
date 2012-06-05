using System.Collections.Generic;

namespace CloakedHipster
{
    public class StyleClass
    {
        public string Name { get; set; }

        public StyleClass()
        {
            Attributes = new SortedList<string, string>();
            Name = string.Empty;
        }

        public SortedList<string, string> Attributes { get; set; }
    }
}