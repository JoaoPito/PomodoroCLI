using Xunit;
using PomodoroCLI.Timer;

namespace PomodoroCLI.Timer.Tests;

public class SessionTimer_Tests
{

    //readonly PomodoroCLI.Timer.Timer _timer;

    bool triggered;

    PomodoroCLI.Timer.Timer CreateTimer(TimeSpan duration) {
        var timer = new PomodoroCLI.Timer.SessionTimer(new System.Timers.Timer(), TimerTrigger);
        timer.SetDuration(duration);
        return timer;
    }

    void TimerTrigger() {
	    triggered = true;
    }

    void ResetTrigger() {
	    triggered = false;
    }

    [Theory]
    [InlineData(0, 0, 10)]
    [InlineData(0, 10, 0)]
    [InlineData(1, 30, 0)]
    public void IfSetDurationCorrectly(int hour, int min, int sec)
    {
        var duration = new TimeSpan(hour,min,sec);
        var timer = CreateTimer(duration);

        var resultSec = Math.Round(timer.GetRemainingTime().TotalSeconds);
        var expectedSec = Math.Round(duration.TotalSeconds);

        Assert.True((resultSec == expectedSec), $"Session duration not set correctly, got {resultSec}, expected {expectedSec}");
    }

    [Fact]
    public void IfTimerTickingCorrectly() {
        var duration = new TimeSpan(0,0,10);
        var waitTime = new TimeSpan(0,0,1);

        var timer = CreateTimer(duration);
        timer.Start();

        Thread.Sleep((int)waitTime.TotalMilliseconds);

        var resultTime = Math.Round(timer.GetRemainingTime().TotalSeconds);
        var expectedTime = Math.Round(duration.TotalSeconds) - Math.Round(waitTime.TotalSeconds);

        Assert.True((resultTime == expectedTime), $"Expected {expectedTime}s, got {resultTime}s");
    }

    [Fact]
    public void TimerIsTriggering() {
        var duration = new TimeSpan(0,0,1);
        var waitTime = new TimeSpan(0,0,2);

        var timer = CreateTimer(duration);
        timer.Start();

        Thread.Sleep((int)waitTime.TotalMilliseconds);

        Assert.True(triggered, "Trigger timeout");
        ResetTrigger();
    }

    [Fact]
    public void TimerProperlyStarted(){
        
    }

    [Fact]
    public void TimerProperlyStopped() {
        // Configure/Start Timer
        // Wait
        // Stop Timer
        // Check
        // Wait
        // Check
    }

    [Fact]
    public void TimerProperlyStartStop() {

    }
}
