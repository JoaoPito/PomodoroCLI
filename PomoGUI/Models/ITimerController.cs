using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomoGUI.Models
{
    internal interface ITimerController
    {
        public void StartWorkTimer(TimeSpan duration);
        public void StartBreakTimer(TimeSpan duration);
        public void PauseCurrentTimer();
        public void SkipCurrentTimer();

    }
}
