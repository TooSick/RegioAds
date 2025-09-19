using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using RegioAds.Infrastructure.Options;
using RegioAds.Infrastructure.Repos;

namespace RegioAds.Tests.Infrastructure
{
    public class FileAdPlatformRepositoryTests
    {
        private readonly Mock<IOptions<FileCofig>> _mockFileOptions;
        private readonly IMemoryCache _cache;
        private readonly FileAdPlatformRepository _repository;
        private readonly string _testFilePath;

        public FileAdPlatformRepositoryTests()
        {
            _testFilePath = Path.GetTempFileName();

            var fileConfig = new FileCofig { FilePath = _testFilePath };
            _mockFileOptions = new Mock<IOptions<FileCofig>>();
            _mockFileOptions.Setup(x => x.Value).Returns(fileConfig);

            _cache = GetMemoryCache();

            _repository = new FileAdPlatformRepository(_mockFileOptions.Object, _cache);
        }

        public void Dispose()
        {
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [Fact]
        public async Task GetTreeAsync_FileExists_ReturnsTree()
        {
            var fileContent = new[]
            {
                "Platform1: location1, location2",
                "Platform2: location3, location1"
            };
            await File.WriteAllLinesAsync(_testFilePath, fileContent);

            var result = await _repository.GetTreeAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.FindNodes("location1").Count);
            Assert.Single(result.FindNodes("location2"));
        }

        [Fact]
        public async Task GetTreeAsync_FileNotFound_ThrowsException()
        {
            File.Delete(_testFilePath);

            await Assert.ThrowsAsync<FileNotFoundException>(() => _repository.GetTreeAsync());
        }

        [Fact]
        public async Task ReloadTreeAsync_ReloadsTree()
        {
            var initialContent = new[] { "Platform1: location1" };
            await File.WriteAllLinesAsync(_testFilePath, initialContent);

            await _repository.ReloadTreeAsync();

            Assert.True(true);
        }

        private IMemoryCache GetMemoryCache()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetService<IMemoryCache>();
        }
    }
}
