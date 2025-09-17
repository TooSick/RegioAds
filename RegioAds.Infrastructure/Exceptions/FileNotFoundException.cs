namespace RegioAds.Infrastructure.Exceptions
{
    public class FileNotFoundException : Exception
    {
        public string FilePath { get; }

        public FileNotFoundException(string filePath)
        {
            FilePath = filePath;
        }
    }
}
