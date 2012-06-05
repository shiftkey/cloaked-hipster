using Xunit;

namespace CloakedHipster.Tests
{
    public class StringExtensionTests
    {
        [Fact]
        public void MapDoubleToHexValue_WithZero_ReturnsTwoCharacters()
        {
            Assert.Equal("00", "0.0".MapDoubleToHexValue());
        }
        [Fact]
        public void MapDoubleToHexValue_WithOne_ReturnsTwoCharacters()
        {
            Assert.Equal("FF", "1.0".MapDoubleToHexValue());
        }
    }
}