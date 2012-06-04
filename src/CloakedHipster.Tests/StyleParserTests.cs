using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CloakedHipster.Tests
{
    public class StyleParserTests
    {
        [Fact]
        public void Parse_WithEmptyValue_ReturnsEmptyResourcesSet()
        {
            var parser = new StyleParser();
            var result = parser.Parse("");
            Assert.Empty(result);
        }
    }

    public class StyleParser
    {
        public IEnumerable<Resource> Parse(string css)
        {
            return Enumerable.Empty<Resource>();
        }
    }

    public class Resource
    {
    }
}
