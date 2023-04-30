namespace PomodoroCLI.CLI;

public class CLI
{

    Timer.SessionTimer timer;

    TimeSpan sessionDuration;
    TimeSpan breakDuration;

    public CLI(string[] args){
        (sessionDuration, breakDuration) = ParseArgs(args);

        var clock = new Timer.SystemTimer();
        timer = new Timer.SessionTimer(clock, OnSessionEnd);

        timer.SetDuration(sessionDuration);
    }

    (TimeSpan, TimeSpan) ParseArgs(string[] args){
        return (TimeSpan.Zero, TimeSpan.Zero);
    }

    void OnSessionEnd(){
        Console.WriteLine($"Session end, duration:{sessionDuration}, break:{breakDuration}");
    }

    public void Start() {
        timer.Start();
    }
}
