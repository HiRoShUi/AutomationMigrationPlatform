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
        protected static ISerializer _serializer;
        protected static IList<IModuleInstance> _moduleList;

        /// <summary>
        /// Inits the serializer as the specified serializer.
        /// </summary>
        /// <param name="aiSerializer"></param>
        public static void InitializeSerializer(ISerializer aiSerializer)
        {
            _serializer = aiSerializer;
        }

        /// <summary>
        /// Creates a config with one dummy-value for the TimerTestModul.
        /// </summary>
        /// <param name="aiModuleInstances"></param>
        /// <param name="aiPath"></param>
        /// <returns></returns>
        public static string CreateConfigAsString(IList<IModuleInstance> aiModuleInstances)
        {
            if (_serializer == null)
                _serializer = new NewtonsoftJsonSerializer();

            string aoText = "";

            var list = new List<Module>();
            foreach (var instance in aiModuleInstances)
                list.Add(new Module(instance.AssemblyName, instance.Type, 1, 1000, instance.Interval, instance.IsBackgroundWorkerActive));
            aoText = _serializer.Serialize(list);
            return aoText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aiPath"></param>
        /// <param name="aiSerializer"></param>
        public static void Execute(string aiPath)
        {
            if (_serializer == null)
                _serializer = new NewtonsoftJsonSerializer();

            if (!File.Exists(aiPath))
                return;

            _moduleList = new List<IModuleInstance>();

            var config = _serializer.Deserialize<List<Module>>(File.ReadAllText(aiPath));

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

        /// <summary>
        /// Executes each module.
        /// </summary>
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
