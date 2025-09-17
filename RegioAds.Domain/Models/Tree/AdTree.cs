namespace RegioAds.Domain.Models.Tree
{
    public class AdTree : PrefixTree<AdPlatform>
    {
        public AdTree() : base(new AdPlatformComparer())
        { }
    }
}
