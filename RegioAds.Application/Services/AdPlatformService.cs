using RegioAds.Application.Abstractions;

namespace RegioAds.Application.Services
{
    public class AdPlatformService : IAdPlatformService
    {
        private readonly IAdPlatformRepository _adPlatformRepository;


        public AdPlatformService(IAdPlatformRepository adPlatformRepository)
        {
            _adPlatformRepository = adPlatformRepository;
        }


        public async Task ReloadTreeAsync()
        {
            await _adPlatformRepository.ReloadTreeAsync();
        }

        public async Task<List<string>> FindPlatformsByLocationAsync(string location)
        {
            var tree = await _adPlatformRepository.GetTreeAsync();
            var platforms = tree.FindNodes(location);

            return platforms.Select(p => p.Name).ToList();
        }
    }
}
