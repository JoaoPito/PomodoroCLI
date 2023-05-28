using System.Timers;

namespace Pomogotchi.Timer {
    public class SessionTimer : ISessionTimer{

        IGenericTimer updateClock;
        TimeSpan clockPeriod = new TimeSpan(100 * TimeSpan.TicksPerMillisecond);

        TimeSpan totalDuration;
	    TimeSpan remainingTime;

	    Action? trigger;
        event Action? updateTriggers;

        public SessionTimer(IGenericTimer clock) {
            this.updateClock = clock;
            updateClock.RegisterNewTriggerEvent(Tick);

            updateTriggers = null;

            Reset();
        }

        public void Reset()
        {
            Stop();
            updateClock.SetInterval(clockPeriod.TotalMilliseconds);
            remainingTime = totalDuration;
        }

        public void Start()
        {
            updateClock.Start();
        }

        public void Stop()
        {
            updateClock.Stop();
        }

        public void SetTrigger(Action trigger) {
            this.trigger = trigger;
        }

        public void RegisterUpdateTrigger(Action trigger)
        {
            updateTriggers += trigger;
        }

        public void SetDuration(TimeSpan duration) {
            if (duration.TotalMilliseconds == 0)
                throw new ArgumentOutOfRangeException("duration", duration.TotalMilliseconds, "Duration time must be higher than 0.");

            this.totalDuration = duration;
            Reset();
        }

	    void Tick(Object? source, System.Timers.ElapsedEventArgs e){
	        remainingTime -= clockPeriod;

            updateTriggers?.Invoke();

            if (remainingTime.TotalSeconds <= 0)
	            Ring();
	    }

        void Ring() {
	        Stop();
	        if(trigger != null) trigger();
	    }

        public TimeSpan GetRemainingTime() {
            return remainingTime;
        }

	    
    }
}
