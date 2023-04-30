using System.Timers;

namespace PomodoroCLI.Timer.Tests {
    public class TimerStub : IGenericTimer {

        ElapsedEventHandler Events;

        bool hasStarted = false;
        TimeSpan interval;
        TimeSpan remainingTime;

        public TimerStub() {
            Events = new ElapsedEventHandler((Object, ElapsedEventArgs) => {return;});
            interval = new TimeSpan();
            remainingTime = new TimeSpan();
        }

        public void SetInterval(double milisseconds) {
            interval = new TimeSpan((long)milisseconds * TimeSpan.TicksPerMillisecond);
            remainingTime = interval;
        }

        public void RegisterNewTriggerEvent(ElapsedEventHandler newEvent) {
            if(Events == null) 
                Events = new ElapsedEventHandler(newEvent);
            else
                Events += newEvent;
        }

        public void Start() {
            hasStarted = true;
        }

        public void Stop() {
            hasStarted = false;
        }

        public void SkipTime(TimeSpan milliseconds) {
            remainingTime -= milliseconds;

            if(remainingTime.TotalMilliseconds <= 0 && hasStarted){
                remainingTime = interval;
                Events(this, new EventArgs() as ElapsedEventArgs);
            }
        }

        public TimeSpan GetRemainingTime() {
            return remainingTime;
        }

        public bool GetHasStarted() {
            return hasStarted;
        }
    }
}