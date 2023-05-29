namespace Pomogotchi.Application.Timer;

using System.Timers;

public interface IGenericTimer {
    public void Start();
    public void Stop();

    public void RegisterNewTriggerEvent(ElapsedEventHandler newEvent);
    public void SetInterval(double millisseconds);
}