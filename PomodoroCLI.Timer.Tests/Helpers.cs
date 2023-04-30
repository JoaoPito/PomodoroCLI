using Xunit;

namespace PomodoroCLI.Timer.Tests;

public class Helpers {
    public static ISessionTimer CreateTimer(IGenericTimer clockTimer, Action? trigger, TimeSpan duration) {
        var timer = new SessionTimer(clockTimer, trigger);
        timer.SetDuration(duration);
        return timer;
    }

    public static void AssertIfTimeMatches(ISessionTimer timer, TimeSpan expected, string message) {
        var resultTime = Math.Round(timer.GetRemainingTime().TotalMilliseconds);
        var expectedTime = Math.Round(expected.TotalMilliseconds);

        Assert.True(resultTime == expectedTime, $"{message} Time does not match. Expected {expectedTime}s, got {resultTime}s");
    }
}