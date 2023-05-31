using Pomogotchi.Domain;

namespace Pomogotchi.Persistence
{
    public interface IConfigLoader
    {
        public Session LoadWorkParams();
        public Session LoadBreakParams();
        public void SaveWorkParams(Session session);
        public void SaveBreakParams(Session session);
        public string GetSoundFilePath();
    }
}