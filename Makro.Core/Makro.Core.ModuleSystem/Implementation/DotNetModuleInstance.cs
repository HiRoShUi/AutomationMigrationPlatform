using Makro.Core.ModuleSystem.HelpClasses;
using Makro.Core.ModuleSystem.nsInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Makro.Core.ModuleSystem.Implementation
{
    public class DotNetModuleInstance : IModuleInstance
    {
        protected string _type;
        protected int _interval;
        protected bool _backgroundWorkerActive;
        private string _assemblyName;
        protected int _delay;

        public DotNetModuleInstance(string aiAssemblyName, string aiType, int aiInterval = 1000, int aiDelay = 0,bool aiBackgroundWorkerActive = false)
        {
            _assemblyName = aiAssemblyName;
            _type = aiType;
            _interval = aiInterval;
            _delay = aiDelay;
            _backgroundWorkerActive = aiBackgroundWorkerActive;
        }

        public int Interval
        {
            get { return _interval; }
        }

        public int Delay
        {
            get { return _delay; }
        }

        public bool IsBackgroundWorkerActive
        {
            get
            {
                return _backgroundWorkerActive;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
        }

        public string AssemblyName
        {
            get
            {
                return _assemblyName;
            }
        }

        public async void Start()
        {
            try
            {
                var task = Task.Factory.StartNew(() =>
                 {
                     Thread.Sleep(Delay);//Wait the delay
                     StartProcess();
                 });

                await task;
            }
            catch (Exception e)
            {
                ModuleLogger.Logger.Log(e,MaKro.Core.Logging.LogStage.DEBUG);
            }
        }

        public void StartProcess()
        {
            //Custom stuff -> delete after debug-phase
            AssemblyContainer container = new AssemblyContainer();
            var instance = container.GetInstance(Environment.CurrentDirectory + "\\bin", AssemblyName, Type);//.CreateInstance(Type);

            //end of custom stuff
            // var instance = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(Type);
            if (instance == null)
                throw new Exception("Instance '" + Type + "' not known! Please specify a valid instance!");//Could be replaced by logging. Currently using the "harder" way.

            ModuleLogger.Logger.Log("Setting interval of the module '" + Type + "' to '" + _interval + "'.", MaKro.Core.Logging.LogStage.DEBUG);
            //set interval
            MethodInfo setIntervalMethod = instance.GetType().GetMethod(MagicNumbersSolver.SetInterval);
            setIntervalMethod.Invoke(instance, new object[] { _interval });

            ModuleLogger.Logger.Log("Setting Backgroundworker-setting of the module '" + Type + "' to '" + _backgroundWorkerActive + "'.", MaKro.Core.Logging.LogStage.DEBUG);
            //set backgroundworker
            MethodInfo setBackgroundWorkerActive = instance.GetType().GetMethod(MagicNumbersSolver.SetBackgroundWorkerActive);
            setBackgroundWorkerActive.Invoke(instance, new object[] { _backgroundWorkerActive });

            ModuleLogger.Logger.Log("Executing the module '" + Type + "'.", MaKro.Core.Logging.LogStage.DEBUG);
            //execute the implementation
            MethodInfo executeMethod = instance.GetType().GetMethod(MagicNumbersSolver.Execute);
            executeMethod.Invoke(instance, null);
        }
    }
}
