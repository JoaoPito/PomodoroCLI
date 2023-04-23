using System.Timers;

namespace PomodoroCLI.Timer {
    public class SessionTimer : PomodoroCLI.Timer.Timer{

        System.Timers.Timer timer;
        TimeSpan setDuration;
        DateTime lastSetTime;

        public SessionTimer() {
            timer = new System.Timers.Timer();
        }

        public void SetDuration(TimeSpan duration) {
            if(duration.TotalMilliseconds > 0){
                timer.Interval = duration.TotalMilliseconds;
                this.setDuration = duration;
                lastSetTime = DateTime.Now;
            }
        }

        public TimeSpan GetRemainingTime() {
            var elapsedTime = DateTime.Now - lastSetTime;
            return setDuration - elapsedTime;
        }

        public void Start() {
            timer.Enabled = true;
        }
    }
}