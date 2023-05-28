using Avalonia.Remote.Protocol.Designer;
using Pomogotchi.Timer;
using PomoGUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomoGUI.Models
{
    public class SessionParams
    {
        public enum SessionType {
            None = 0,
            Work,
            Break
        }

        SessionType _type = SessionType.None;

        public SessionType Type { get => _type; }

        TimeSpan _duration;

        public TimeSpan Duration{
            get { return _duration; }
        }

        public SessionParams(TimeSpan duration, SessionType type)
        {
            _type = type;
            _duration = duration;
        }

    }
}
