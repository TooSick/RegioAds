using RegioAds.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace RegioAds.Domain.Models
{
    public class AdPlatform
    {
        public string Name { get; private set; }

        public AdPlatform(string value)
        {
            Validate(value);
            Name = value;
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidAdPlatformNameException();
        }
    }

    public class AdPlatformComparer : IEqualityComparer<AdPlatform>
    {
        public bool Equals(AdPlatform? x, AdPlatform? y)
        {
            if (x == null || y == null)
                return false;

            return x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] AdPlatform obj)
        {
            return obj.GetHashCode();
        }
    }
}
