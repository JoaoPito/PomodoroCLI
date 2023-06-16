using FluentValidation;

namespace Pomogotchi.Application.SoundPlayer
{
    public static class SoundFileHandler
    {
        const string INVALID_FILE_ERROR_MESSAGE = "Invalid media file path";
        const string FILE_NOT_FOUND_ERROR_MESSAGE = "Could not find file: ";
        const string UNSUPPORTED_FILE_EXTENSION_ERROR_MESSAGE = "Cannot open file with extension: ";

        static readonly string[] SUPPORTED_FILE_FORMATS = { ".mp3", ".ogm", ".ogg", ".wav", ".a52", ".dts", ".aac", ".flac", ".dv", ".vid" };

        public class FilePathValidator : AbstractValidator<string>
        {
            public FilePathValidator()
            {
                RuleFor(path => path).NotEmpty()
                                    .Must(BeAValidFilePath)
                                    .WithMessage(INVALID_FILE_ERROR_MESSAGE);

                RuleFor(path => path).Must(Exist)
                                    .WithMessage(FILE_NOT_FOUND_ERROR_MESSAGE);

                RuleFor(path => path).Must(HaveValidExtension)
                                    .WithMessage(UNSUPPORTED_FILE_EXTENSION_ERROR_MESSAGE);
            }

            private bool BeAValidFilePath(string path)
            {
                return (path != null && Path.HasExtension(path));
            }

            private bool HaveValidExtension(string path)
            {
                var extension = Path.GetExtension(path);
                return SUPPORTED_FILE_FORMATS.Contains(extension.ToLower());
            }

            bool Exist(string path)
            {
                return Path.Exists(path);
            }
        }
    }
}