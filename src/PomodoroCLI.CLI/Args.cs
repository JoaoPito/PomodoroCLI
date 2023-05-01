namespace PomodoroCLI.UI;

public class Args{
    public TimeSpan sessionDuration {get;}
    public TimeSpan breakDuration {get;}

    public Args() {
        sessionDuration = TimeSpan.Zero;
        breakDuration = TimeSpan.Zero;
    }

    public Args(TimeSpan sessionDuration, TimeSpan breakDuration){
        this.sessionDuration = sessionDuration;
        this.breakDuration = breakDuration;
    }
}