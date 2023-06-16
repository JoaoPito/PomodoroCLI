using FluentValidation;

namespace Pomogotchi.Application.ConfigLoader
{
    public class FileHandler
    {
        public class ConfigFileValidator : AbstractValidator<string>
        {
            public ConfigFileValidator()
            {
                RuleFor(path => path).NotEmpty()
                                    .Must(BeAValidPath)
                                    .Must(Exist)
                                    .Must(HaveValidExtension);
            }

            private bool BeAValidPath(string path)
            {
                return (path != null && Path.GetInvalidFileNameChars().Count() == 0);
            }

            private bool HaveValidExtension(string path)
            {
                string extension = Path.GetExtension(path);
                return (Path.HasExtension(path) && extension.ToLower() == ".json");
            }

            private bool Exist(string path)
            {
                return File.Exists(path);
            }
        }
    }
}