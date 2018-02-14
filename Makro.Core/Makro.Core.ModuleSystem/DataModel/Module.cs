using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.ModuleSystem.DataModel
{ 
    [Serializable]
    public class Module
    {
        public string AssemblyName { get; set; }
        public string StartupClass { get; set; }
        public int Count { get; set; }
        public int Delay { get; set; }
        public int Interval { get; set; }
        public bool BackgroundWorker { get; set; }
        
        public Module(string aiAssemblyName, string aiStartupClass, int aiCount, int aiDelay, int aiInterval, bool aiBackgroundWorker)
        {
            AssemblyName = aiAssemblyName;
            StartupClass = aiStartupClass;
            Count = aiCount;
            Delay = aiDelay;
            Interval = aiInterval;
            BackgroundWorker = aiBackgroundWorker;
        }


        public Module()
        {

        }        
    }
}
