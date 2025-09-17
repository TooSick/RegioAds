namespace RegioAds.Application.Abstractions
{
    public interface IAdPlatformService
    {
        Task ReloadTreeAsync();
        Task<List<string>> FindPlatformsByLocationAsync(string location);
    }
}
