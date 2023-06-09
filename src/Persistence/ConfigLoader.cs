using Pomogotchi.Domain;

namespace Pomogotchi.Persistence
{
    public class ConfigLoader : IConfigLoader
    {
        static ConfigLoader? _instance;

        private ConfigLoader() { }

        public static ConfigLoader GetController()
        {
            if(_instance == null)
                _instance = new ConfigLoader();

            return _instance;
        }

        public Session LoadWorkParams()
        {
            TimeSpan duration = LoadWorkDuration();
            Session loadedSession = new Session(duration);
            return loadedSession;
        }

        public Session LoadBreakParams()
        {
            TimeSpan duration = LoadBreakDuration();
            Session loadedSession = new Session(duration);
            return loadedSession;
        }

        TimeSpan LoadWorkDuration()
        {
            return new TimeSpan(0, 0, 10);
        }

        TimeSpan LoadBreakDuration()
        {
            return new TimeSpan(0, 5, 0);
        }

        public void SaveWorkParams(Session session)
        {
            throw new NotImplementedException();
        }

        public void SaveBreakParams(Session session)
        {
            throw new NotImplementedException();
        }

        public string GetSoundFilePath()
        {
            return "./ding.wav";
        }
    }
}