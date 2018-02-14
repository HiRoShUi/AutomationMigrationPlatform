using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.ModuleSystem.nsInterfaces
{
    public interface IBackgroundWorker
    {
        int Interval { get; set; }
        Func<object> Function { get; set; } 
        void Start();
        void Stop();
        object GetResults();
    }
}
