using Makro.Core.ModuleSystem.DataModel;
using Makro.Core.ModuleSystem.Implementation;
using Makro.Core.ModuleSystem.nsInterfaces;
using Makro.Core.Serialization.Implementation;
using Makro.Core.Serialization.nsInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.ModuleSystem
{
    public class StartUp
    {
        protected static ISerializer _serializer = new NewtonsoftJsonSerializer();
        protected static IList<IModuleInstance> _moduleList;

        /// <summary>
        /// Creates a config with one dummy-value for the TimerTestModul.
        /// </summary>
        /// <param name="aiModuleInstances"></param>
        /// <param name="aiPath"></param>
        /// <returns></returns>
        public static string CreateConfigAsString(IList<IModuleInstance> aiModuleInstances)
        {
            string aoText = "";

            var list = new List<Module>();
            foreach (var instance in aiModuleInstances)
                list.Add(new Module(instance.AssemblyName, instance.Type, 1, 1000, instance.Interval, instance.IsBackgroundWorkerActive));
            aoText = _serializer.Serialize(list);
            return aoText;
        }

        public static void Execute(string aiPath)
        {
            if (!File.Exists(aiPath))
                return;

            _moduleList = new List<IModuleInstance>();

            var config = _serializer.Deserialize<IList<Module>>(File.ReadAllText(aiPath));

            config
                .ToList()
                .ForEach(module =>
                {
                    for(int i = 0;i < module.Count; i++)
                    {
                        _moduleList.Add(new DotNetModuleInstance(module.AssemblyName, module.StartupClass, module.Interval, module.Delay, module.BackgroundWorker));
                    }
                });

            ExecuteModules();
        }

        private static void ExecuteModules()
        {
            foreach(var module in _moduleList)
            {
                //ToDo: Logging?
                module.Start();
            }
        }
    }
}
