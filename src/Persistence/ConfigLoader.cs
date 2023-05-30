using Pomogotchi.Domain;

namespace Pomogotchi.Persistence
{
    public class ConfigLoader : IConfigLoader
    {
        static ConfigLoader? controllerInstance;

        private ConfigLoader() { }

        public static ConfigLoader GetController()
        {
            if(controllerInstance == null)
                controllerInstance = new ConfigLoader();

            return controllerInstance;
        }

        public Session LoadWorkSession()
        {
            TimeSpan duration = LoadWorkDuration();
            Session loadedSession = new Session(duration, Session.SessionType.Work);
            return loadedSession;
        }

        public Session LoadBreakSession()
        {
            TimeSpan duration = LoadBreakDuration();
            Session loadedSession = new Session(duration, Session.SessionType.Break);
            return loadedSession;
        }

        TimeSpan LoadWorkDuration()
        {
            return new TimeSpan(0, 25, 0);
        }

        TimeSpan LoadBreakDuration()
        {
            return new TimeSpan(0, 5, 0);
        }

        public void SaveWorkSession(Session session)
        {
            throw new NotImplementedException();
        }

        public void SaveBreakSession(Session session)
        {
            throw new NotImplementedException();
        }

        public string GetSoundFilePath()
        {
            return "./ding.wav";
        }
    }
}