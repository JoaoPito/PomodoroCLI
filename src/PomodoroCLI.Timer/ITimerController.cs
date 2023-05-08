using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomoGUI.Models
{
    internal interface ITimerController
    {
        public void StartWorkSession(TimeSpan duration);
        public void StartBreakSession(TimeSpan duration);
        public void PauseCurrentSession();
        public void SkipCurrentSession();
    }
}
