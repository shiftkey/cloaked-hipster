using Xunit;

namespace CloakedHipster.Tests
{
    public class CssProcessorTests
    {
        [Fact]
        public void Process_BackgroundByName_UsesCorrectCasing()
        {
            var input = "titletext {  background: blue; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"Blue\"/>" +
                           "</Style>";

            var processor = new CssProcessor(new CssParser(), new BackgroundMapper());
            var output = processor.Process(input);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_BackgroundAsHex_MapsToARGBValue()
        {
            var input = "titletext {  background: #45aa34; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"#FF45AA34\"/>" +
                           "</Style>";

            var processor = new CssProcessor(new CssParser(), new BackgroundMapper());
            var output = processor.Process(input);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_BackgroundColorWithoutOpacity_MapsToARGBValue()
        {
            var input = "titletext { background-color: rgb(255, 255, 255); }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"#FFFFFFFF\"/>" +
                           "</Style>";

            var processor = new CssProcessor(new CssParser(), new BackgroundMapper());
            var output = processor.Process(input);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_BackgroundColorWithOpacity_MapsToARGBValue()
        {
            var input = "titletext { background-color: rgba(255, 255, 255, 0.6);}";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"#FFFFFFFF\"/>" +
                           "</Style>";

            var processor = new CssProcessor(new CssParser(), new BackgroundMapper());
            var output = processor.Process(input);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }
    }
}