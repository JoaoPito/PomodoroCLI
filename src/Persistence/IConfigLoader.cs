using Pomogotchi.Domain;

namespace Pomogotchi.Persistence
{
    public interface IConfigLoader
    {
        public Session LoadWorkSession();
        public Session LoadBreakSession();
        public void SaveWorkSession(Session session);
        public void SaveBreakSession(Session session);
        public string GetSoundFilePath();
    }
}