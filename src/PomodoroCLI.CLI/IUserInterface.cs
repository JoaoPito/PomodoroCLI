using PomodoroCLI.Timer;
namespace PomodoroCLI.UI;

public abstract class IUserInterface {
    public IUserInterface(ISessionTimer timer){
        timer.SetTrigger(OnSessionEnd);
    }
    public abstract void Start();
    protected abstract void OnSessionEnd();
}