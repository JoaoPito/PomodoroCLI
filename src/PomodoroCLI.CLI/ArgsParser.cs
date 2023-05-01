namespace PomodoroCLI.UI;

public static class ArgsParser{
    public static Args Parse(string[] args){
        return LoadDefaults();
    }

    public static Args LoadDefaults(){
        var sessionDur = new TimeSpan(45 * TimeSpan.TicksPerMinute);
        var breakDur = new TimeSpan(15 * TimeSpan.TicksPerMinute);
        return new Args(sessionDur, breakDur);
    }
}