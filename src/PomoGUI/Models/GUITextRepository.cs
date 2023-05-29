namespace PomoGUI.Models
{
    internal class GUITextRepository
    {
        public readonly string StartWorkSessionTxt = "Start Working";
        public readonly string StartBreakSessionTxt = "Start Break";
        public readonly string SkipSessionTxt = "Skip";

        static GUITextRepository? _instance;

        private GUITextRepository() { }

        public static GUITextRepository GetRepository()
        {
            if (_instance == null)
            {
                _instance = new GUITextRepository();
            }

            return _instance;
        }
    }
}
