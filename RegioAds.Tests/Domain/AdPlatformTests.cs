using RegioAds.Domain.Exceptions;
using RegioAds.Domain.Models;

namespace RegioAds.Tests.Domain
{
    public class AdPlatformTests
    {
        [Fact]
        public void Constructor_ValidName_SetsName()
        {
            var name = "TestPlatform";
            var platform = new AdPlatform(name);

            Assert.Equal(name, platform.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_InvalidName_ThrowsException(string invalidName)
        {
            Assert.Throws<InvalidAdPlatformNameException>(() => new AdPlatform(invalidName));
        }
    }

    public class AdPlatformComparerTests
    {
        private readonly AdPlatformComparer _comparer = new();

        [Fact]
        public void Equals_SameNames_ReturnsTrue()
        {
            var platform1 = new AdPlatform("Test");
            var platform2 = new AdPlatform("Test");

            var result = _comparer.Equals(platform1, platform2);

            Assert.True(result);
        }

        [Fact]
        public void Equals_DifferentNames_ReturnsFalse()
        {
            var platform1 = new AdPlatform("Test1");
            var platform2 = new AdPlatform("Test2");

            var result = _comparer.Equals(platform1, platform2);

            Assert.False(result);
        }

        [Fact]
        public void Equals_OneNull_ReturnsFalse()
        {
            var platform = new AdPlatform("Test");

            Assert.False(_comparer.Equals(platform, null));
            Assert.False(_comparer.Equals(null, platform));
            Assert.False(_comparer.Equals(null, null));
        }

        [Fact]
        public void GetHashCode_ReturnsHashCode()
        {
            var platform = new AdPlatform("Test");

            var hashCode = _comparer.GetHashCode(platform);

            Assert.NotEqual(0, hashCode);
        }
    }
}
