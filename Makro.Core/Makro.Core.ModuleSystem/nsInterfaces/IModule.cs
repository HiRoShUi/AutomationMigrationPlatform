using Makro.Core.ModuleSystem.HelpClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.ModuleSystem.nsInterfaces
{
    public interface IModule: IDisposable
    {
        void Execute();
        void SetIntervalOfBackgroundWorker(int aiInterval);
        void SetBackgroundWorkerActive(bool aiIsBackgroundWorkerActive);
        object FunctionImplementation();
    }
}
