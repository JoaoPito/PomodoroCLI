namespace Pomogotchi.API.FunctionalTests;
using Pomogotchi.API.Builders;
using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions;
using Pomogotchi.Domain;

public class TimerSetupAndUsageFuncTests
{
    [Fact]
    public void CanBuildAPI()
    {
        var builder = new ApiControllerBuilder();
        var result = builder.GetController();

        Assert.True(result is ApiControllerBase);
    }

    [Fact]
    public void APIUsageTest()
    {
        bool hasTriggered = false;

        // Ferb want to use this new cool pomodoro timer API he found on the internet
        // He builds a controller with only the basic features
        var builder = new ApiControllerBuilder();
        ApiControllerBase pomogotchi = builder.GetController();

        // Then, he sets up the controller to trigger a special method in his code when a session ends 
        var pomogotchiSession = (SessionExtensionController)(pomogotchi.GetExtension(typeof(SessionExtensionController)));
        pomogotchiSession.EndTriggers += (() => hasTriggered = true);

        // Ferb then sets up his work timer to start a new work session
        // And proceeds to set the session duration for 5s long and break to 3s
        var workDuration = new TimeSpan(0, 0, 5);
        var breakDuration = new TimeSpan(0, 0, 3);

        var pomogotchiConfig = (ConfigLoaderExtension)(pomogotchi.GetExtension(typeof(ConfigLoaderExtension)));

        pomogotchiConfig.SetWorkParams(new Session(workDuration));
        pomogotchiConfig.SetBreakParams(new Session(breakDuration));
        var workSession = new Extensions.SessionExtension.WorkSession(pomogotchiConfig.GetWorkParameters());
        pomogotchiSession.SwitchSessionTo(workSession);

        // He checks if the sessions had been set correctly
        Assert.True(pomogotchiSession.Session.Parameters.Duration == workDuration, "Work timer has not been set correctly");

        // He checks if the timer is going to start a work session and if the duration is correct
        Assert.Equal(typeof(Extensions.SessionExtension.WorkSession), pomogotchiSession.Session.GetType());
        Assert.True(pomogotchiSession.Duration == workDuration,
                    $"Current session duration was not set properly, got {pomogotchiSession.Session.Parameters.Duration}, expected {workDuration}");
        Assert.Equal(workDuration, pomogotchiSession.Timer.GetRemainingTime());

        // He then starts it
        pomogotchiSession.Start();
        // Ferb checks if the timer has properly started and is properly counting down
        var waitTime = new TimeSpan(500 * TimeSpan.TicksPerMillisecond);
        var tolerance = new TimeSpan(100 * TimeSpan.TicksPerMillisecond);

        Assert.True(pomogotchiSession.InSession, "Work session has not been started properly");
        TestsIfSessionIsCountingDown(pomogotchiSession, waitTime, tolerance);

        // Then, he waits for it to finish
        // When the session ends he checks if the method he configured earlier has triggered properly
        Thread.Sleep((int)((workDuration - waitTime) + tolerance).TotalMilliseconds);
        AssertThenClearFlag(hasTriggered, "Timer has not triggered properly");

        // He then starts another session
        pomogotchiSession.SwitchSessionTo(pomogotchiSession.Session.GetNextSession());
        pomogotchiSession.Session.LoadConfig(pomogotchiConfig);
        // Since he eager to finish all his sessions, he checks if it's really a break session
        Assert.Equal(typeof(Extensions.SessionExtension.BreakSession), pomogotchiSession.Session.GetType());
        Assert.True(pomogotchiSession.Session.Parameters.Duration == breakDuration, "Break timer has not been set correctly");

        // He stops the break session midway
        pomogotchiSession.Stop();
        // He checks if it really stopped
        var beforeWaitingTime = pomogotchiSession.Timer.GetRemainingTime();
        Thread.Sleep(waitTime);
        var afterWaitingTime = pomogotchiSession.Timer.GetRemainingTime();

        Assert.Equal((double)beforeWaitingTime.Milliseconds, (double)afterWaitingTime.Milliseconds, (double)tolerance.Milliseconds);    

        // He realized that it's summer and he doesn't need to work today
    }

    void TestsIfSessionIsCountingDown(SessionExtensionController pomogotchiSession, TimeSpan waitDuration, TimeSpan expectedTolerance)
    {
        var beforeWaitingTime = pomogotchiSession.Timer.GetRemainingTime();
        Thread.Sleep(waitDuration);
        var afterWaitingTime = pomogotchiSession.Timer.GetRemainingTime();

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