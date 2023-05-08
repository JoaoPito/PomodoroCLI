using Avalonia.Remote.Protocol.Designer;
using PomodoroCLI.Timer;
using PomoGUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomoGUI.Models
{
    public class Session
    {
        public enum SessionType {
            None = 0,
            Work,
            Break
        }

        SessionType _currentSession = SessionType.None;

        public SessionType CurrentSession => _currentSession;

        TimeSpan _duration;

        public TimeSpan Duration{
            get { return _duration; }
        }

        public Session(TimeSpan duration, SessionType type)
        {
            _currentSession = type;
            _duration = duration;
        }

    }
}
