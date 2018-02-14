using Makro.Core.ModuleSystem.HelpClasses;
using Makro.Core.ModuleSystem.nsInterfaces;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.ModuleSystem.Implementation
{
    [Serializable]
    public class TimerTestModul : IModule
    {
        private int _interval;
        protected IBackgroundWorker _worker;
        private bool _isBackgroundWorkerActive;

        //Own variables
        protected string _currentname;

        public void Execute()
        {
            _currentname = "No Name Set";

            if (_isBackgroundWorkerActive == false)//If no worker -> just do the implementation now!
                FunctionImplementation();
            else//if we are using an worker -> start the worker
            {
                _worker = new TimerBackgroundWorker(_interval);
                if (_interval == 0)
                    _worker = new TimerBackgroundWorker();
                _worker.Function = () => FunctionImplementation();
                _worker.Start();
            }
        }

        /// <summary>
        /// Function that will run when the backgroundworker ticks.
        /// </summary>
        /// <returns></returns>
        public object FunctionImplementation()
        {
            Console.WriteLine("Your last name was: " + _currentname);
            Console.Write("Please insert your name: ");
            _currentname = Console.ReadLine();
            return _currentname;
        }

        public void SetIntervalOfBackgroundWorker(int aiInterval)
        {
            _interval = aiInterval;
        }

        public void SetBackgroundWorkerActive(bool aiIsWorkerActive)
        {
            _isBackgroundWorkerActive = aiIsWorkerActive;
        }

        public void Dispose()
        {
            _interval = 0;
            _isBackgroundWorkerActive = false;
            _worker.Stop();
            _worker = null;
        }
    }
}
