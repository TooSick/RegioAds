using Moq;
using RegioAds.Application.Abstractions;
using RegioAds.Application.Services;
using RegioAds.Domain.Models;
using RegioAds.Domain.Models.Tree;

namespace RegioAds.Tests.Application
{
    public class AdPlatformServiceTests
    {
        private readonly Mock<IAdPlatformRepository> _mockRepository;
        private readonly AdPlatformService _service;

        public AdPlatformServiceTests()
        {
            _mockRepository = new Mock<IAdPlatformRepository>();
            _service = new AdPlatformService(_mockRepository.Object);
        }

        [Fact]
        public async Task ReloadTreeAsync_CallsRepository()
        {
            _mockRepository.Setup(x => x.ReloadTreeAsync())
                         .Returns(Task.CompletedTask);

            await _service.ReloadTreeAsync();

            _mockRepository.Verify(x => x.ReloadTreeAsync(), Times.Once);
        }

        [Fact]
        public async Task FindPlatformsByLocationAsync_ReturnsPlatformNames()
        {
            var tree = new AdTree();
            tree.AddNode("location1", new AdPlatform("Platform1"));
            tree.AddNode("location1", new AdPlatform("Platform2"));

            _mockRepository.Setup(x => x.GetTreeAsync())
                         .ReturnsAsync(tree);

            var result = await _service.FindPlatformsByLocationAsync("location1");

            Assert.Equal(2, result.Count);
            Assert.Contains("Platform1", result);
            Assert.Contains("Platform2", result);
        }

        [Fact]
        public async Task FindPlatformsByLocationAsync_NoPlatforms_ReturnsEmptyList()
        {
            var tree = new AdTree();
            _mockRepository.Setup(x => x.GetTreeAsync())
                         .ReturnsAsync(tree);

            var result = await _service.FindPlatformsByLocationAsync("nonexistent");

            Assert.Empty(result);
        }
    }
}
