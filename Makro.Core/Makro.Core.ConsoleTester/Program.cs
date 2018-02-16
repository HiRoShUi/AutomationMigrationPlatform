using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makro.Core.ModuleSystem;
using Makro.Core.ModuleSystem.nsInterfaces;
using Makro.Core.ModuleSystem.Implementation;
using Makro.Core.Settings.nsInterfaces;
using Makro.Core.Settings.Implementation;
using System.Threading;
using Makro.Core.Serialization.Implementation;
using System.IO;
using Makro.Core.Webservice;
using System.Net.Http;
using System.Reflection;

namespace Makro.Core.ConsoleTester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ISettings settings = new JsonFileSettings("Testsettings.json", new NewtonsoftJsonSerializer());
            //settings.CreateSettingProperties(new List<Property>());
            //settings.LoadSettingProperties();
            //int interval = Convert.ToInt32(settings.RetrieveProperty("CurrentModule_Interval").Value);
            //bool isWorkerActive = Convert.ToBoolean(settings.RetrieveProperty("CurrentModule_BackgroundWorkerActive").Value);
            //string assemblyName = settings.RetrieveProperty("CurrentModule_AssemblyName").Value.ToString();
            //IModuleInstance moduleInstance = new DotNetModuleInstance(assemblyName, settings.RetrieveProperty("CurrentModule_Name").Value.ToString(), interval, isWorkerActive);
            //moduleInstance.Start();

            Makro.Core.Webservice.Implementation.Webservice service = new Webservice.Implementation.Webservice();

            var assembly = Assembly.LoadFrom(@"C:\Users\Administrator\Documents\Visual Studio 2015\Git\AutomationMigrationPlatform\Makro.Core\Makro.Core.ConsoleTester\bin\Makro.Core.ConsoleTester.dll");
            service.AddControllerAssembly(assembly);
            service.Start(Webservice.Implementation.Enums.Protocol.http,"localhost",5112);

            //Makro.Core.ModuleSystem.StartUp.Execute("ModuleConfig.conf");

            //Check if multithreading is active
            while (true)
            {
                Console.WriteLine("Multithreading active");
                Thread.Sleep(10000);
            }
            //Console.ReadKey();
        }

        private static void CreateModuleConfig()
        {
            IList<IModuleInstance> instances = new List<IModuleInstance>();
            IModuleInstance instance = new DotNetModuleInstance("Makro.Core.ModuleSystem.dll", "Makro.Core.ModuleSystem.Implementation.TimerTestModul", 10000, 0, true);
            instances.Add(instance);
            var test = Makro.Core.ModuleSystem.StartUp.CreateConfigAsString(instances);
            File.WriteAllText("ModuleConfig.conf", test);
        }
    }
}
