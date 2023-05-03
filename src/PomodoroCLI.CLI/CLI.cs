using PomodoroCLI.Console;
using PomodoroCLI.Timer;

namespace PomodoroCLI.UI;

public class CLI : IUserInterface
{
    IConsoleBase console;
    ISessionTimer timer;

    TimeSpan sessionDuration;
    TimeSpan breakDuration;
    
    public CLI(Args args, IConsoleBase console, ISessionTimer timer) : base(timer){
        this.console = console;
        
        sessionDuration = args.sessionDuration;
        breakDuration = args.breakDuration;

        this.timer = timer;

        timer.SetDuration(sessionDuration);
    }

    public override void Start() {
        OnSessionStart();
        timer.Start();
    }

    protected override void OnSessionEnd(){
        console.WriteLine($"Session end, duration:{sessionDuration}, break:{breakDuration}");
    }

    void OnSessionStart(){
        var clock = GetRemainingTimeClock(timer);
        console.WriteLine($"{clock}\n[Press Spacebar]");
    }

    string GetRemainingTimeClock(ISessionTimer timer){
        var time = timer.GetRemainingTime();
        return String.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
    }

}
