using Microsoft.AspNetCore.Mvc;
using Moq;
using RegioAds.Api.Controllers;
using RegioAds.Application.Abstractions;

namespace RegioAds.Tests.Api
{
    public class AdPlatformControllerTests
    {
        private readonly Mock<IAdPlatformService> _mockService;
        private readonly AdPlatformController _controller;

        public AdPlatformControllerTests()
        {
            _mockService = new Mock<IAdPlatformService>();
            _controller = new AdPlatformController(_mockService.Object);
        }

        [Fact]
        public async Task Reload_CallsService_ReturnsOk()
        {
            _mockService.Setup(x => x.ReloadTreeAsync())
                      .Returns(Task.CompletedTask);

            var result = await _controller.Reload();

            Assert.IsType<OkResult>(result);
            _mockService.Verify(x => x.ReloadTreeAsync(), Times.Once);
        }

        [Fact]
        public async Task FindPlatforms_ValidLocation_ReturnsPlatforms()
        {
            var platforms = new List<string> { "Platform1", "Platform2" };
            _mockService.Setup(x => x.FindPlatformsByLocationAsync("location1"))
                      .ReturnsAsync(platforms);

            var result = await _controller.FindPlatforms("location1");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPlatforms = Assert.IsType<List<string>>(okResult.Value);
            Assert.Equal(2, returnedPlatforms.Count);
        }

        [Fact]
        public async Task FindPlatforms_EmptyLocation_ReturnsEmptyList()
        {
            _mockService.Setup(x => x.FindPlatformsByLocationAsync(""))
                      .ReturnsAsync(new List<string>());

            var result = await _controller.FindPlatforms("");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPlatforms = Assert.IsType<List<string>>(okResult.Value);
            Assert.Empty(returnedPlatforms);
        }
    }
}
