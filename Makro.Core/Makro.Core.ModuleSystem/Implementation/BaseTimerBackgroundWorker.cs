using Makro.Core.ModuleSystem.HelpClasses;
using Makro.Core.ModuleSystem.nsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.ModuleSystem.Implementation
{
    [Serializable]
    /// <summary>
    /// BaseClass that uses a System.Timers.Timer if Backgroundworking should be enabled
    /// </summary>
    public abstract class BaseTimerBackgroundWorker: IModule
    { 
        protected int _interval;
        protected bool _backgroundWorkerActive;
        private IBackgroundWorker _worker;

        public void Execute()
        {
            if (_interval == 0)
                _interval = 10000;

            if (!_backgroundWorkerActive)
                FunctionImplementation();
            else
            {
                _worker = new TimerBackgroundWorker(_interval);
                _worker.Function = () => FunctionImplementation();
                _worker.Start();
            }
        }

        public abstract object FunctionImplementation();

        public void SetBackgroundWorkerActive(bool aiIsBackgroundWorkerActive)
        {
            _backgroundWorkerActive = aiIsBackgroundWorkerActive;
        }

        public void SetIntervalOfBackgroundWorker(int aiInterval)
        {
            _interval = aiInterval;
        }

        public void Dispose()
        {
            _interval = 0;
            _backgroundWorkerActive = false;
            _worker.Stop();
            _worker = null;
        }
    }
}
