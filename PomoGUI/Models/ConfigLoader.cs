using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomoGUI.Models
{
    internal class ConfigLoader : IConfigLoader
    {
        static ConfigLoader? controllerInstance;

        private ConfigLoader() { }

        public static ConfigLoader GetController()
        {
            if(controllerInstance == null)
                controllerInstance = new ConfigLoader();

            return controllerInstance;
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
            return new TimeSpan(0, 0, 10);
        }

        TimeSpan LoadBreakDuration()
        {
            return new TimeSpan(0, 0, 15);
        }

        public void SaveWorkSession(SessionParams session)
        {
            throw new NotImplementedException();
        }

        public void SaveBreakSession(SessionParams session)
        {
            throw new NotImplementedException();
        }
    }
}
