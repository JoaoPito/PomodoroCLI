namespace Pomogotchi.API.FunctionalTests;
using Pomogotchi.API.Builders;
using Pomogotchi.API.Controllers;
using Pomogotchi.Domain;

public class TimerSetupAndUsageFuncTests
{
    [Fact]
    public void CanBuildAPI()
    {
        var builder = new SessionsControllerBuilder();
        var result = builder.GetController();

        Assert.True(result is ApiControllerBase);
    }

    [Fact]
    public void APIUsageTest()
    {
        bool hasTriggered = false;

        // Ferb want to use this new cool pomodoro timer API he found on the internet
        // He builds a controller with only the basic features
        var builder = new SessionsControllerBuilder();
        ApiControllerBase pomogotchi = builder.GetController();

        // Then, he sets up the controller to trigger a special method in his code when a session ends 
        pomogotchi.EndTriggers += (() => hasTriggered = true);

        // Ferb then sets up his work timer to start a new work session
        // And proceeds to set the session duration for 5s long and break to 3s
        var workDuration = new TimeSpan(0, 0, 5);
        var breakDuration = new TimeSpan(0, 0, 3);

        pomogotchi.SetSessionDuration(Session.SessionType.Work, workDuration);
        pomogotchi.SetSessionDuration(Session.SessionType.Break, breakDuration);
        // He checks if the sessions had been set correctly
        Assert.True(pomogotchi.WorkSession.Duration == workDuration, "Work timer has not been set correctly");
        Assert.True(pomogotchi.BreakSession.Duration == breakDuration, "Break timer has not been set correctly");

        // He checks if the timer is going to start a work session and if the duration is correct
        Assert.True(pomogotchi.CurrentSession.Type == Session.SessionType.Work,
                    $"The current session type is not what was expected, got {pomogotchi.CurrentSession.Type}, expected {Session.SessionType.Work}");
        Assert.True(pomogotchi.CurrentSession.Duration == workDuration,
                    $"Current session duration was not set properly, got {pomogotchi.CurrentSession.Duration}, expected {workDuration}");

        // He then starts it
        pomogotchi.StartSession();
        // Ferb checks if the timer has properly started and is properly counting down
        var waitTime = new TimeSpan(500 * TimeSpan.TicksPerMillisecond);
        var tolerance = new TimeSpan(100 * TimeSpan.TicksPerMillisecond);

        Assert.True(pomogotchi.InSession, "Work session has not been started properly");
        TestsIfSessionIsCountingDown(pomogotchi, waitTime, tolerance);

        // Then, he waits for it to finish
        // When the session ends he checks if the method he configured earlier has triggered properly
        Thread.Sleep((int)((workDuration - waitTime) + tolerance).TotalMilliseconds);
        AssertThenClearFlag(hasTriggered, "Timer has not triggered properly");

        // He then starts another session
        pomogotchi.SwitchSessionTo(Session.SessionType.Break);
        // Since he eager to finish all his sessions, he checks if it's really a break session
        Assert.Equal(Session.SessionType.Break, pomogotchi.CurrentSession.Type);

        // He stops the break session midway
        pomogotchi.StopSession();
        // He checks if it really stopped
        var beforeWaitingTime = pomogotchi.Timer.GetRemainingTime();
        Thread.Sleep(waitTime);
        var afterWaitingTime = pomogotchi.Timer.GetRemainingTime();

        Assert.Equal((double)beforeWaitingTime.Milliseconds, (double)afterWaitingTime.Milliseconds, (double)tolerance.Milliseconds);    

        // He realized that it's summer and he doesn't need to work today
    }

    void TestsIfSessionIsCountingDown(ApiControllerBase pomogotchi, TimeSpan waitDuration, TimeSpan expectedTolerance)
    {
        var beforeWaitingTime = pomogotchi.Timer.GetRemainingTime();
        Thread.Sleep(waitDuration);
        var afterWaitingTime = pomogotchi.Timer.GetRemainingTime();

        var expectedRemainingTime = beforeWaitingTime - waitDuration;
        Assert.Equal((double)expectedRemainingTime.Milliseconds, (double)afterWaitingTime.Milliseconds, (double)expectedTolerance.Milliseconds);
    }

    TimeSpan WaitTime(TimeSpan wait, TimeSpan total)
    {
        Thread.Sleep(wait);
        return total - wait;
    }

    void AssertThenClearFlag(bool flag, string message)
    {
        Assert.True(flag, message);
        flag = false;
    }
}