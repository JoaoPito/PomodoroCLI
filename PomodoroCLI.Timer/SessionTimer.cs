using System.Timers;

namespace PomodoroCLI.Timer {
    public class SessionTimer : PomodoroCLI.Timer.Timer{

        System.Timers.Timer clock;
        TimeSpan totalDuration;
	    TimeSpan remainingTime;
        TimeSpan clockPeriod = new TimeSpan(100 * TimeSpan.TicksPerMillisecond);

	    Action trigger;

        public SessionTimer(System.Timers.Timer timer, Action trigger) {
            this.clock = timer;
            this.clock.Elapsed += Tick;
            this.trigger = trigger;

            Reset();
        }

        public void SetDuration(TimeSpan duration) {
            this.totalDuration = duration;
            Reset();
        }

	    void Tick(Object source, System.Timers.ElapsedEventArgs e){
	        remainingTime -= clockPeriod;

	        if(remainingTime.TotalSeconds <= 0)
	            Ring();
	    }

        void Ring() {
	        Stop();
	        trigger();
	    }

        public TimeSpan GetRemainingTime() {
            return remainingTime;
        }

	    public void Reset() {
            Stop();
            clock.Interval = clockPeriod.TotalMilliseconds;	    
	        remainingTime = totalDuration;
	    }

        public void Start() {
            clock.Start();
        }

	    public void Stop() {
            clock.Stop();
	    }
    }
}
