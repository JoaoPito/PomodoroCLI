using PomodoroCLI.Console;
using PomodoroCLI.Timer;

namespace PomodoroCLI.UI;

public class CLI
{
    IConsoleBase console;
    SessionTimer timer;

    TimeSpan sessionDuration;
    TimeSpan breakDuration;
    
    public CLI(Args args, IConsoleBase console){
        this.console = console;
        
        sessionDuration = args.sessionDuration;
        breakDuration = args.breakDuration;

        var clock = new Timer.SystemTimer();
        timer = new Timer.SessionTimer(clock, OnSessionEnd);

        timer.SetDuration(sessionDuration);
    }

    void OnSessionEnd(){
        console.WriteLine($"Session end, duration:{sessionDuration}, break:{breakDuration}");
    }

    void OnSessionStart(){
        var clock = GetRemainingTimeClock(timer);
        console.WriteLine($"{clock}\n[Press Spacebar]");
    }

    public void Start() {
        OnSessionStart();
        timer.Start();
    }

    string GetRemainingTimeClock(SessionTimer timer){
        var time = timer.GetRemainingTime();
        return String.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
    }
}
