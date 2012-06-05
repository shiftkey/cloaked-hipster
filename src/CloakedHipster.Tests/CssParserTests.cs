using System.Linq;
using Xunit;

namespace CloakedHipster.Tests
{
    public class CssParserTests
    {
        [Fact]
        public void Parse_WithEmptyValue_ReturnsEmptyResourcesSet()
        {
            var parser = new CssParser();
            var result = parser.Parse("");
            Assert.Empty(result);
        }

        [Fact]
        public void Parse_WithAClassAndNoContents_ReturnsOneItem()
        {
            var parser = new CssParser();
            var result = parser.Parse("foo { }");
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Parse_WithMultipleValues_UsesLastValue()
        {
            var parser = new CssParser();
            var result = parser.Parse("foo { key: thing; key: another-thing; }");
            Assert.Equal("another-thing", result.First().Attributes["key"]);
        }
    }
}
