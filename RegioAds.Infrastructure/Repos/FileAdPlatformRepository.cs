using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RegioAds.Application.Abstractions;
using RegioAds.Domain.Models;
using RegioAds.Domain.Models.Tree;
using RegioAds.Infrastructure.Options;

namespace RegioAds.Infrastructure.Repos
{
    public class FileAdPlatformRepository : IAdPlatformRepository
    {
        private const string CACHE_KEY = "AdPlatformTree";

        private readonly IMemoryCache _cache;
        private readonly FileCofig _fileCofig;

        public FileAdPlatformRepository(IOptions<FileCofig> fileOptions, IMemoryCache cache)
        {
            _fileCofig = fileOptions.Value;
            _cache = cache;
        }

        public async Task<AdTree> GetTreeAsync()
        {
            if (_cache.TryGetValue<AdTree>(CACHE_KEY, out var tree) && tree != null)
                return tree;

            tree = await LoadTreeFromFileAsync();
            _cache.Set(CACHE_KEY, tree);

            return tree;
        }

        public async Task ReloadTreeAsync()
        {
            var tree = await LoadTreeFromFileAsync();
            _cache.Set(CACHE_KEY, tree);
        }

        private async Task<AdTree> LoadTreeFromFileAsync()
        {
            if (!File.Exists(_fileCofig.FilePath))
                throw new FileNotFoundException(_fileCofig.FilePath);

            var tree = new AdTree();

            foreach (var line in await File.ReadAllLinesAsync(_fileCofig.FilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(':');
                if (parts.Length != 2)
                    continue;

                var platformName = parts[0].Trim();
                var platform = new AdPlatform(platformName);
                var locations = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(l => l.Trim());

                foreach (var location in locations)
                    tree.AddNode(location, platform);
            }

            return tree;
        }
    }
}
