using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomoGUI.Models
{
    public interface IConfigLoader
    {
        public SessionParams LoadWorkSession();
        public SessionParams LoadBreakSession();
        public void SaveWorkSession(SessionParams session);
        public void SaveBreakSession(SessionParams session);


    }
}
