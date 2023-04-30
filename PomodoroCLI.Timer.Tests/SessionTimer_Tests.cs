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

        AssertIfTimeMatches(timer, duration, "Session duration not set correctly");
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

        AssertIfTimeMatches(timer, duration - waitTime, "");
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
        var duration = new TimeSpan(0,0,10);
        var wait = new TimeSpan(0,0,1);

        var clockStub = new TimerStub();
        var timer = CreateTimer(clockStub, null, duration);
        timer.Start();

        // Wait
        clockStub.SkipTime(wait);
        // Stop Timer
        timer.Stop();
        // Check
        AssertIfTimeMatches(timer, duration - wait, "1st:");

        // Wait
        clockStub.SkipTime(wait);
        // Check
        AssertIfTimeMatches(timer, duration - wait, "2nd:");
    }

    [Fact]
    public void TimerProperlyStartStop() {

    }

    void AssertIfTimeMatches(ISessionTimer timer, TimeSpan expected, string message) {
        var resultTime = Math.Round(timer.GetRemainingTime().TotalMilliseconds);
        var expectedTime = Math.Round(expected.TotalMilliseconds);

        Assert.True(resultTime == expectedTime, $"{message} Time does not match. Expected {expectedTime}s, got {resultTime}s");
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