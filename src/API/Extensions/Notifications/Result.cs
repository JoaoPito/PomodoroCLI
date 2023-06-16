namespace Pomogotchi.API.Extensions
{
    public class CommandResult
    {
        public bool Successful { get; protected set; }
        public string Error { get; protected set; } = "";
        public static CommandResult Success() => new CommandResult { Successful = true };
        public static CommandResult Failure(string errorMsg) => new CommandResult { Successful = false, Error = errorMsg };
    }
}