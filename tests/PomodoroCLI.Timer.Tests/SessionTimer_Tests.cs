using Xunit;
using Pomogotchi.Timer;

namespace Pomogotchi.Timer.Tests;

public class SessionTimer_Tests
{
    [Theory]
    [InlineData(0, 0, 10)]
    [InlineData(0, 10, 0)]
    [InlineData(1, 30, 0)]
    public void IfSetDurationCorrectly(int hour, int min, int sec)
    {
        var clockStub = new TimerStub();
        var duration = new TimeSpan(hour,min,sec);
        var timer = Helpers.CreateTimer(clockStub, null, duration);

        Helpers.AssertIfTimeMatches(timer, duration, "Session duration not set correctly");
    }

    [Fact]
    public void TimerProperlyStarted(){

    }

    [Fact]
    public void IfTimerTickingCorrectly() {
        var clockStub = new TimerStub();
        var duration = new TimeSpan(0,0,10);
        var waitTime = new TimeSpan(0,0,1);

        var timer = Helpers.CreateTimer(clockStub, null, duration);
        timer.Start();
        
        clockStub.SkipTime(waitTime);
        //Thread.Sleep((int)waitTime.TotalMilliseconds);

        Helpers.AssertIfTimeMatches(timer, duration - waitTime, "");
    }

    [Theory]
    [InlineData(0,0,1)]
    [InlineData(0, 45, 0)]
    [InlineData(1, 30, 0)]
    public void TimerIsTriggeringProperly(int hours, int minutes, int seconds) {
        var clockStub = new TimerStub();
        bool triggered = false;

        var duration = new TimeSpan(hours,minutes,seconds);
        var waitTime = new TimeSpan(hours,minutes,seconds);

        var timer = Helpers.CreateTimer(clockStub, () => triggered=true, duration);
        timer.Start();

        clockStub.SkipTime(waitTime);

        Assert.True(triggered, $"Trigger timeout, clock remaining time: {clockStub.GetRemainingTime()}");
    }

    [Fact]
    public void TimerThrowsExceptionWhenDurationIsZero()
    {
        var clockStub = new TimerStub();
        bool triggered = false;

        var duration = new TimeSpan(0, 0, 0);
        const string expectedMessage = "Duration time must be higher than 0.";

        try
        {
            var timer = Helpers.CreateTimer(clockStub, () => triggered = true, duration);
        } 
        catch (ArgumentOutOfRangeException e)
        {
            Assert.Contains(expectedMessage, e.Message);
            return;
        }

        Assert.Fail("The Timer did not throw the expected exception.");
    }

    [Fact]
    public void TimerProperlyStopped() {
        // Configure/Start Timer
        var duration = new TimeSpan(0,0,10);
        var wait = new TimeSpan(0,0,1);

        var clockStub = new TimerStub();
        var timer = Helpers.CreateTimer(clockStub, null, duration);
        timer.Start();

        // Wait
        clockStub.SkipTime(wait);
        // Stop Timer
        timer.Stop();
        // Check
        Helpers.AssertIfTimeMatches(timer, duration - wait, "1st timer didnt stop");

        // Wait
        clockStub.SkipTime(wait);
        // Check
        Helpers.AssertIfTimeMatches(timer, duration - wait, "2nd timer didnt stop");
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