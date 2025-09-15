namespace RegioAds.Domain.Exceptions
{
    public class InvalidRegionValueException : Exception
    {
        private const string DEFAULT_MESSAGE = "Region can't be empty";

        public InvalidRegionValueException(string? message = DEFAULT_MESSAGE) : base(message)
        { }
    }
}
