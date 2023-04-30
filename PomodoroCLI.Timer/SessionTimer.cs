using System.Timers;

namespace PomodoroCLI.Timer {
    public class SessionTimer : ISessionTimer{

        IGenericTimer updateClock;
        TimeSpan clockPeriod = new TimeSpan(100 * TimeSpan.TicksPerMillisecond);

        TimeSpan totalDuration;
	    TimeSpan remainingTime;

	    Action? trigger;

        public SessionTimer(IGenericTimer clock, Action? trigger) {
            this.updateClock = clock;
            this.trigger = trigger;
            updateClock.RegisterNewTriggerEvent(Tick);

            Reset();
        }

        public void SetDuration(TimeSpan duration) {
            this.totalDuration = duration;
            Reset();
        }

	    void Tick(Object? source, System.Timers.ElapsedEventArgs e){
	        remainingTime -= clockPeriod;

	        if(remainingTime.TotalSeconds <= 0)
	            Ring();
	    }

        void Ring() {
	        Stop();
	        if(trigger != null) trigger();
	    }

        public TimeSpan GetRemainingTime() {
            return remainingTime;
        }

	    public void Reset() {
            Stop();
            updateClock.SetInterval(clockPeriod.TotalMilliseconds);   
	        remainingTime = totalDuration;
	    }

        public void Start() {
            updateClock.Start();
        }

	    public void Stop() {
            updateClock.Stop();
	    }
    }
}
