namespace Pomogotchi.API.Extensions.Notifications
{
    public class Result
    {
        public bool Successful { get; protected set; }
        public string Error { get; protected set; } = "";
        public static Result Success() => new Result { Successful = true };
        public static Result Failure(string errorMsg) => new Result { Successful = false, Error = errorMsg };
    }
}