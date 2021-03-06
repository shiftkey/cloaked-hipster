using System;

namespace CloakedHipster
{
    public abstract class SimpleMapper : IMapper
    {
        readonly string key;
        readonly string property;

        protected SimpleMapper(string key, string property)
        {
            this.key = key;
            this.property = property;
        }

        public bool IsMatch(StyleClass styleClass)
        {
            return styleClass.Attributes.ContainsKey(key);
        }

        public Tuple<string, string> Process(StyleClass styleClass)
        {
            string value;
            if (styleClass.Attributes.TryGetValue(key, out value))
            {
                return new Tuple<string, string>(property, value);
            }
            return null;
        }
    }
}