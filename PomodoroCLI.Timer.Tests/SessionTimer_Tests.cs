using Xunit;
using PomodoroCLI.Timer;

namespace PomodoroCLI.Timer.Tests;

public class SessionTimer_Tests
{

    readonly PomodoroCLI.Timer.Timer _timer;

    public SessionTimer_Tests() {
        _timer = new PomodoroCLI.Timer.SessionTimer();
    }

    [Theory]
    [InlineData(0, 0, 10)]
    [InlineData(0, 10, 0)]
    [InlineData(1, 30, 0)]
    public void SetDuration_0sElapsed(int hour, int min, int sec)
    {
        var duration = new TimeSpan(hour,min,sec);
        _timer.SetDuration(duration);

        var resultSec = Math.Round(_timer.GetRemainingTime().TotalSeconds);
        var expectedSec = Math.Round(duration.TotalSeconds);

        Assert.True((resultSec == expectedSec), $"Session duration not set correctly, got {resultSec}, expected {expectedSec}");
    }

    [Fact]
    public void SetDuration_afterTimeElapsed() {
        var duration = new TimeSpan(0,0,10);
        var waitTime = new TimeSpan(0,0,1);

        _timer.SetDuration(duration);
        _timer.Start();

        Thread.Sleep((int)waitTime.TotalMilliseconds);

        var resultTime = Math.Round(_timer.GetRemainingTime().TotalSeconds);
        var expectedTime = Math.Round(duration.TotalSeconds) - Math.Round(waitTime.TotalSeconds);

        Assert.True((resultTime == expectedTime), $"Expected {expectedTime}s, got {resultTime}s");
    }

    [Fact]
    public void Timer_ProperlyStarted(){

    }

    [Fact]
    public void Timer_ProperlyStopped() {
        // Configure/Start Timer
        // Wait
        // Stop Timer
        // Check
        // Wait
        // Check
    }

    [Fact]
    public void Timer_ProperlyStartStop() {

    }
}