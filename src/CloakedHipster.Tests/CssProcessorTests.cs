using Xunit;

namespace CloakedHipster.Tests
{
    public class CssProcessorTests
    {
        [Fact]
        public void Process_SimpleMarkup_ReturnsCorrectSyntax()
        {
            var input = "titletext {  background: blue; }";
            var expected = "<Style x:Key=\"titletext\"><Setter Property=\"Background\" Value=\"Blue\"/></Style>";

            var processor = new CssProcessor(new CssParser(), new ConventionMapper());
            var output = processor.Process(input);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

    }
}