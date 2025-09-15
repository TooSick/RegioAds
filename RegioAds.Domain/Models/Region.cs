using RegioAds.Domain.Exceptions;

namespace RegioAds.Domain.Models
{
    public class Region
    {
        public string Value { get; private set; }

        public Region(string value)
        {
            Validate(value);
            Value = value;
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidRegionValueException();
        }
    }
}
