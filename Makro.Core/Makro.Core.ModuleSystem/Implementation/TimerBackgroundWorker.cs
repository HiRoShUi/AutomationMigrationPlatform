using Makro.Core.ModuleSystem.nsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Makro.Core.ModuleSystem.Implementation
{
    public class TimerBackgroundWorker : IBackgroundWorker
    {
        protected Timer _timer;
        protected object _result;

        public int Interval { get; set; }

        public TimerBackgroundWorker(int aiInterval = 1000)
        {
            _timer = null;
            _result = null;
            Interval = aiInterval;
        }

        public Func<object> Function { get; set; }
  
        public void Start()
        {
            _timer = new Timer(10);
            _timer.Elapsed += new ElapsedEventHandler(Tick);
            _timer.Start();
        }

        private void Tick(Object source, ElapsedEventArgs e)
        {
            _timer.Stop();
            _timer.Interval = Interval;

            var obj = Function.Invoke();
            _result = obj;

            _timer.Start();
        }

        public object GetResults()
        {
            return _result;
        }

        public void Stop()
        {
            if (_timer == null)
                return;
            _timer.Stop();
        }
    }
}
