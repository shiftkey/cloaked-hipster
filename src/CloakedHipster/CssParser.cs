// adapted from https://gist.github.com/561823

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CloakedHipster
{
    public class CssParser
    {
        public IEnumerable<StyleClass> Parse(string contents)
        {
            var content = CleanUp(contents);
            var parts = content.Split('}');
            foreach (var s in parts)
            {
                if (CleanUp(s).IndexOf('{') > -1)
                {
                    yield return Map(s);
                }
            }
        }

        private static StyleClass Map(string s)
        {
            var parts = s.Split('{');
            var styleName = CleanUp(parts[0]).Trim().ToLower();

            var sc = new StyleClass { Name = styleName };

            var atrs = CleanUp(parts[1]).Replace("}", "").Split(';');
            foreach (var a in atrs)
            {
                if (!a.Contains(":")) continue;

                var key = a.Split(':')[0].Trim().ToLower();
                if (sc.Attributes.ContainsKey(key))
                {
                    sc.Attributes.Remove(key);
                }
                sc.Attributes.Add(key, a.Split(':')[1].Trim());
            }

            return sc;
        }

        private static string CleanUp(string s)
        {
            var temp = s;
            var reg = "(/\\*(.|[\r\n])*?\\*/)|(//.*)";
            var r = new Regex(reg);
            temp = r.Replace(temp, "");
            temp = temp.Replace("\r", "").Replace("\n", "");
            return temp;
        }
    }
}