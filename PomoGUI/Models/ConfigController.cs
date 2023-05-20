using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomoGUI.Models
{
    internal class ConfigController
    {
        static ConfigController? controllerInstance;

        private ConfigController() { }

        public static ConfigController GetController()
        {
            if(controllerInstance == null)
                controllerInstance = new ConfigController();

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
            return new TimeSpan(0, 1, 0);
        }

        TimeSpan LoadBreakDuration()
        {
            return new TimeSpan(0, 0, 15);
        }
    }
}
