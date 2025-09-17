using RegioAds.Domain.Models.Tree;

namespace RegioAds.Application.Abstractions
{
    public interface IAdPlatformRepository
    {
        Task<AdTree> GetTreeAsync();
        Task ReloadTreeAsync();
    }
}
