namespace Pomogotchi.Application.ConfigLoader
{
    public interface IConfigLoader
    {
        public void ReloadConfig();
        public void LoadDefaults();
        public void SaveChanges();
        //public string GetParam(string key);
        //public void SetParam(string key, string value);

        public T GetParamAs<T>(string key);
        public void SetParamAs<T>(string key, T data);
    }
}