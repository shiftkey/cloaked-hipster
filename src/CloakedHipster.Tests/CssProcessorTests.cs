using System.Collections.Generic;
using Xunit;

namespace CloakedHipster.Tests
{
    public class CssProcessorTests
    {
        readonly CssProcessor subject;
        private readonly Conventions conventions;

        public CssProcessorTests()
        {
            conventions = new Conventions();
            
            var mappers = new List<IMapper>
                              {
                                  new BackgroundMapper(), 
                                  new FontSizeMapper(), 
                                  new FontFamilyMapper(), 
                                  new MarginMapper()
                              };

            subject = new CssProcessor(new CssParser(), mappers);
        }

        [Fact]
        public void Process_BackgroundByName_UsesCorrectCasing()
        {
            var input = "titletext {  background: blue; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"Blue\"/>" +
                           "</Style>";

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_BackgroundAsHex_MapsToARGBValue()
        {
            var input = "titletext {  background: #45aa34; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"#FF45AA34\"/>" +
                           "</Style>";

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_BackgroundColorWithoutOpacity_MapsToARGBValue()
        {
            var input = "titletext { background-color: rgb(255, 255, 255); }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"#FFFFFFFF\"/>" +
                           "</Style>";

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_BackgroundColorWithOpacity_MapsToARGBValue()
        {
            var input = "titletext { background-color: rgba(255, 255, 255, 0.1);}";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Background\" Value=\"#19FFFFFF\"/>" +
                           "</Style>";

            var output = subject.Process(input, conventions);

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

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_MarginLeft_MapsCorrectly()
        {
            var input = "titletext {  margin-left: 10px; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Margin\" Value=\"10,0,0,0\"/>" +
                           "</Style>";

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_MarginTop_MapsCorrectly()
        {
            var input = "titletext {  margin-top: 10px; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Margin\" Value=\"0,10,0,0\"/>" +
                           "</Style>";

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }


        [Fact]
        public void Process_MarginBottom_MapsCorrectly()
        {
            var input = "titletext {  margin-bottom: 10px; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Margin\" Value=\"0,0,0,10\"/>" +
                           "</Style>";

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_MarginLeftAndRight_MapsCorrectly()
        {
            var input = "titletext {  margin-left: 10px; margin-right: 10px }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Margin\" Value=\"10,0,10,0\"/>" +
                           "</Style>";

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_Margin_ReordersParametersCorrectly()
        {
            // CSS: top, right, bottom, left
            // XAML: left, top, right, bottom

            var input = "titletext {  margin: 1px 2px 3px 4px; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Margin\" Value=\"4,1,2,3\"/>" +
                           "</Style>";

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_MarginWithShortCut_ReordersParametersCorrectly()
        {
            // CSS: top, right, bottom, left
            // XAML: left, top, right, bottom

            var input = "titletext {  margin: 1px 2px; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Margin\" Value=\"2,1,2,1\"/>" +
                           "</Style>";

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_MarginWithThreeParameters_ReordersParametersCorrectly()
        {
            // CSS: top, right, bottom, left
            // XAML: left, top, right, bottom

            var input = "titletext {  margin:10px 5px 15px; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Margin\" Value=\"5,10,5,15\"/>" +
                           "</Style>";

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

        [Fact]
        public void Process_MarginWithOverride_ReordersParametersCorrectly()
        {
            // CSS: top, right, bottom, left
            // XAML: left, top, right, bottom

            var input = "titletext {  margin:10px; margin-left: 20px; }";
            var expected = "<Style x:Key=\"titletext\">" +
                           "    <Setter Property=\"Margin\" Value=\"20,10,10,10\"/>" +
                           "</Style>";

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }



        [Fact]
        public void Process_WithConventionSpecified_IncludesTargetType()
        {
            // CSS: top, right, bottom, left
            // XAML: left, top, right, bottom

            var input = "titletext {  margin:10px; margin-left: 20px; }";
            var expected = "<Style x:Key=\"titletext\" TargetType=\"TextBlock\">" +
                           "    <Setter Property=\"Margin\" Value=\"20,10,10,10\"/>" +
                           "</Style>";

            conventions.Use("text", "TextBlock");

            var output = subject.Process(input, conventions);

            Assert.Equal(expected, output.IgnoreWhiteSpace());
        }

    }
}