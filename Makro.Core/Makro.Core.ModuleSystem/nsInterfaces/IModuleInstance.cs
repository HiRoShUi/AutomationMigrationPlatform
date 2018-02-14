using MaKro.Core.Logging.nsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.ModuleSystem.nsInterfaces
{
    public interface IModuleInstance
    {
        string Type { get; }
        string AssemblyName { get; }
        int Interval { get; }
        int Delay { get; }
        bool IsBackgroundWorkerActive { get; }
        void Start();
        void StartProcess();
    }
}
