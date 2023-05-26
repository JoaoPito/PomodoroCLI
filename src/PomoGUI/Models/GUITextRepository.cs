using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomoGUI.Models
{
    internal class GUITextRepository
    {
        public readonly string StartWorkSessionTxt = "Start Working";
        public readonly string StartBreakSessionTxt = "Start Break";
        public readonly string SkipSessionTxt = "Skip";

        static GUITextRepository? instance;

        private GUITextRepository() { }

        public static GUITextRepository GetRepository()
        {
            if(instance == null)
            {
                instance = new GUITextRepository();
            }

            return instance;
        }
    }
}
