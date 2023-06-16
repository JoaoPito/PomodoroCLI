namespace Pomogotchi.Application.SoundPlayer
{
    public static class SoundFileHandler
    {
        const string INVALID_FILE_ERROR_MESSAGE = "Invalid media file path";
        const string FILE_NOT_FOUND_ERROR_MESSAGE = "Could not find file: ";
        const string UNSUPPORTED_FILE_EXTENSION_ERROR_MESSAGE = "Cannot open file with extension: ";

        static readonly string[] SUPPORTED_FILE_FORMATS = { ".mp3", ".ogm", ".ogg", ".wav", ".a52", ".dts", ".aac", ".flac", ".dv", ".vid" };

        public static void ValidateSoundFilePath(string path)
        {
            CheckIfPathIsNotEmpty(path);
            CheckIfFormatIsSupported(path);
            CheckIfFileExists(path);
        }

        static void CheckIfPathIsNotEmpty(string path)
        {
            if (path == null || path == string.Empty || !Path.HasExtension(path))
                throw new ArgumentException(INVALID_FILE_ERROR_MESSAGE);
        }

        static void CheckIfFormatIsSupported(string path)
        {
            var extension = Path.GetExtension(path);
            if (!SUPPORTED_FILE_FORMATS.Contains(extension.ToLower()))
                throw new ArgumentException(UNSUPPORTED_FILE_EXTENSION_ERROR_MESSAGE + $"'{extension}'");
        }

        static void CheckIfFileExists(string path)
        {
            if (!Path.Exists(path))
                throw new ArgumentException(FILE_NOT_FOUND_ERROR_MESSAGE + $"'{path}'");
        }
    }
}