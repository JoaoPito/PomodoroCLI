using Xunit;
using PomodoroCLI.Timer;

namespace PomodoroCLI.Timer.Tests;

public class SessionTimer_Tests
{
    ISessionTimer CreateTimer(IGenericTimer clockTimer, Action? trigger, TimeSpan duration) {
        var timer = new SessionTimer(clockTimer, trigger);
        timer.SetDuration(duration);
        return timer;
    }

    [Theory]
    [InlineData(0, 0, 10)]
    [InlineData(0, 10, 0)]
    [InlineData(1, 30, 0)]
    public void IfSetDurationCorrectly(int hour, int min, int sec)
    {
        var clockStub = new TimerStub();

        var duration = new TimeSpan(hour,min,sec);
        var timer = CreateTimer(clockStub, null, duration);

        var resultSec = Math.Round(timer.GetRemainingTime().TotalSeconds);
        var expectedSec = Math.Round(duration.TotalSeconds);

        Assert.True((resultSec == expectedSec), $"Session duration not set correctly, got {resultSec}, expected {expectedSec}");
    }

    [Fact]
    public void TimerProperlyStarted(){

    }

    [Fact]
    public void IfTimerTickingCorrectly() {
        var clockStub = new TimerStub();
        var duration = new TimeSpan(0,0,10);
        var waitTime = new TimeSpan(0,0,1);

        var timer = CreateTimer(clockStub, null, duration);
        timer.Start();
        
        clockStub.SkipTime(waitTime);
        //Thread.Sleep((int)waitTime.TotalMilliseconds);

        var resultTime = Math.Round(timer.GetRemainingTime().TotalSeconds);
        var expectedTime = Math.Round(duration.TotalSeconds) - Math.Round(waitTime.TotalSeconds);

        Assert.True((resultTime < expectedTime), $"Time not decreasing. Expected {expectedTime}s, got {resultTime}s");
    }

    [Theory]
    [InlineData(0,0,1)]
    public void TimerIsTriggering(int hours, int minutes, int seconds) {
        var clockStub = new TimerStub();
        bool triggered = false;

        var duration = new TimeSpan(hours,minutes,seconds);
        var waitTime = new TimeSpan(hours,minutes,seconds);

        var timer = CreateTimer(clockStub, () => triggered=true, duration);
        timer.Start();

        clockStub.SkipTime(waitTime);

        Assert.True(triggered, $"Trigger timeout, clock remaining time: {clockStub.GetRemainingTime()}");
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

    // Stub testing and others
    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(500)]
    public void IfTimerStubTriggerIsWorking(long interval) {
        bool triggered = false;

        void HasTriggered(Object? sender, System.Timers.ElapsedEventArgs a) {
            triggered = true;
        }

        var clockStub = new TimerStub();
        clockStub.RegisterNewTriggerEvent(HasTriggered);
        clockStub.SetInterval(interval);
        clockStub.Start();

        for(var i = 0; i < 5; i++){
            clockStub.SkipTime(new TimeSpan(interval * TimeSpan.TicksPerMillisecond));
            Assert.True(triggered, $"TimerStub didn't trigger for the {i}th time, remaining time: {clockStub.GetRemainingTime()}");
        }
        
    }

    [Fact]
    public void IfTimerStubTriggerStoppedProperly() {
        bool triggered = false;

        void HasTriggered(Object? sender, System.Timers.ElapsedEventArgs a) {
            triggered = true;
        }

        var clockStub = new TimerStub();
        clockStub.RegisterNewTriggerEvent(HasTriggered);
        clockStub.SetInterval(100);

        clockStub.Start();
        clockStub.SkipTime(new TimeSpan(50 * TimeSpan.TicksPerMillisecond));
        clockStub.Stop();

        for(var i = 0; i < 5; i++){
            clockStub.SkipTime(new TimeSpan(50 * TimeSpan.TicksPerMillisecond));
            Assert.False(triggered, $"TimerStub didn't trigger for the {i}th time, remaining time: {clockStub.GetRemainingTime()}");
        }
        
    }
}
