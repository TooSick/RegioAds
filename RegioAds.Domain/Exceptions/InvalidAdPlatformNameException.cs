namespace RegioAds.Domain.Exceptions
{
    public class InvalidAdPlatformNameException : Exception
    {
        private const string DEFAULT_MESSAGE = "Name for ads platform can not be empty";

        public InvalidAdPlatformNameException(string? message = DEFAULT_MESSAGE) : base(message)
        { }
    }
}
