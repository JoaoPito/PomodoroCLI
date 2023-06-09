using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomogotchi.Domain
{
    public class ConfigParams
    {
        public TimeSpan WorkDuration { get; set; }
        public TimeSpan BreakDuration { get; set; }
        public Dictionary<string, string> Extensions { get; set; } = new();
    }
}