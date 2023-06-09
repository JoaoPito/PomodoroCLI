namespace Pomogotchi.Domain
{
    public class Session
    {
        TimeSpan _duration;
        public TimeSpan Duration => _duration;

        public Session(TimeSpan duration)
        {
            _duration = duration;
        }
    }
}