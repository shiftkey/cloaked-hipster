using Xunit;

namespace CloakedHipster.Tests
{
    public class CssProcessorTests
    {
        [Fact]
        public void Process_BackgroundColorByName_UsesCorrectCasing()
        {
            var input = "titletext {  background: blue; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"Blue\"/>" +
                           "</Style>";

            var processor = new CssProcessor(new CssParser(), new ConventionMapper());
            var output = processor.Process(input);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_BackgroundColorAsHex_MapsToARGBValue()
        {
            var input = "titletext {  background: #45aa34; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"#FF45AA34\"/>" +
                           "</Style>";

            var processor = new CssProcessor(new CssParser(), new ConventionMapper());
            var output = processor.Process(input);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

    }
}