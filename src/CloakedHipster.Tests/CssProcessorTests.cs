using System.Collections.Generic;
using Xunit;

namespace CloakedHipster.Tests
{
    public class CssProcessorTests
    {
        readonly CssProcessor subject;

        public CssProcessorTests()
        {
            var mappers = new List<IMapper> { new BackgroundMapper(), new FontSizeMapper(), new FontFamilyMapper() };

            subject = new CssProcessor(new CssParser(), mappers);
        }
        
        [Fact]
        public void Process_BackgroundByName_UsesCorrectCasing()
        {
            var input = "titletext {  background: blue; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"Blue\"/>" +
                           "</Style>";

            var output = subject.Process(input);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_BackgroundAsHex_MapsToARGBValue()
        {
            var input = "titletext {  background: #45aa34; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"#FF45AA34\"/>" +
                           "</Style>";

            var output = subject.Process(input);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_BackgroundColorWithoutOpacity_MapsToARGBValue()
        {
            var input = "titletext { background-color: rgb(255, 255, 255); }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"#FFFFFFFF\"/>" +
                           "</Style>";

            var output = subject.Process(input);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_BackgroundColorWithOpacity_MapsToARGBValue()
        {
            var input = "titletext { background-color: rgba(255, 255, 255, 0.1);}";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"#19FFFFFF\"/>" +
                           "</Style>";

            var output = subject.Process(input);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_FontSize_ProvidesMultipleProperties()
        {
            var input = "titletext {  font-size: 18; font-family: Trebuchet MS; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"FontSize\" Value=\"18\"/>" +
                           "    <Setter Property=\"FontFamily\" Value=\"Trebuchet MS\"/>" +
                           "</Style>";

            var output = subject.Process(input);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }
    }
}