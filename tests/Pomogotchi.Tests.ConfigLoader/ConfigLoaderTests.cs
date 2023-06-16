using Pomogotchi.Application.ConfigLoader;

namespace Pomogotchi.Tests.ConfigLoader
{
    public class ConfigLoaderTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("./no_extension")]
        [InlineData("./wrong_extension.txt")]
        public void TestLoadsDefaultsWhenGivenInvalidFilePath(string path)
        {

        }

        [Fact]
        public void TestSettingParamCorrectly()
        {
            // Create ConfigLoader with FileHandler mock
            // Set param using ConfigLoader
            // Check if is set on mock
        }
    }
}