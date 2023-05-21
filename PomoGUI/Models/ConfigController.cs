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

        public SessionParams LoadNoneSession()
        {
            TimeSpan duration = LoadWorkDuration();
            SessionParams loadedSession = new SessionParams(duration, SessionParams.SessionType.None);
            return loadedSession;
        }

        public SessionParams LoadWorkSession()
        {
            TimeSpan duration = LoadWorkDuration();
            SessionParams loadedSession = new SessionParams(duration, SessionParams.SessionType.Work);
            return loadedSession;
        }

        public SessionParams LoadBreakSession()
        {
            TimeSpan duration = LoadBreakDuration();
            SessionParams loadedSession = new SessionParams(duration, SessionParams.SessionType.Break);
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
