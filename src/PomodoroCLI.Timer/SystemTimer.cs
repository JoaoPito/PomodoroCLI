namespace PomodoroCLI.Timer;

using System.Timers;

public class SystemTimer : IGenericTimer{
    
    Timer timer = new Timer();
    public SystemTimer(){
        
    }

    public SystemTimer(double millisseconds){
        SetInterval(millisseconds);
    }

    public void SetInterval(double millisseconds) {
        timer.Interval = millisseconds;
    }

    public void RegisterNewTriggerEvent(ElapsedEventHandler newEvent) {
        timer.Elapsed += newEvent;
    }

    public void Start() {
        timer.Start();
    }

    public void Stop() {
        timer.Stop();
    }
}