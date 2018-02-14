using Makro.Core.ModuleSystem.nsInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Makro.Core.ModuleSystem.Implementation
{
    public class AssemblyContainer : MarshalByRefObject, IExtensionProxy
    {
        public List<AppDomain> Assemblies
        {
            get; private set;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public AssemblyContainer()
        {
            Assemblies = new List<AppDomain>();
        }

        /// <summary>
        /// This method loads an local assembly to a new created AppDomain
        /// </summary>
        /// <param name="aiAssemblyPath">The path to the Assembly</param>
        /// <param name="aiAssemblyName">The Name of the Assembly</param>
        /// <param name="aiTypeName"The Type Name of the Assembly></param>>
        /// <returns></returns>
        private AppDomain LoadAssemblyToDomain(string aiAssemblyPath, string aiAssemblyName, string aiTypeName)
        {
            var domainSetup = new AppDomainSetup
            {
                ApplicationBase = aiAssemblyPath,
                ShadowCopyFiles = "true"
            };

            var assemblyDomain = AppDomain.CreateDomain(aiAssemblyName, null, domainSetup);
            assemblyDomain.Load(File.ReadAllBytes(aiAssemblyPath + "\\" + aiAssemblyName));

            Assemblies.Add(assemblyDomain);

            return assemblyDomain;
        }

        public object GetInstance(string aiAssemblyPath, string aiAssemblyName, string aiTypeName)
        {
            var assemblyDomain = Assemblies.FirstOrDefault(searchPluginAssembly => searchPluginAssembly.FriendlyName == aiAssemblyName);

            if (assemblyDomain != null)
                return assemblyDomain.CreateInstanceFromAndUnwrap(aiAssemblyPath + aiAssemblyName, aiTypeName) as Assembly;

            var test = LoadAssemblyToDomain(aiAssemblyPath, aiAssemblyName, aiTypeName).CreateInstanceFromAndUnwrap(aiAssemblyPath + "\\" + aiAssemblyName, aiTypeName);
            return test;// Assembly.GetAssembly(test.GetType());
        }

        public void Unload(string aiAssemblyName)
        {
            var targetDomain = Assemblies.FirstOrDefault(unloadDomain => unloadDomain.FriendlyName == aiAssemblyName);

            if (targetDomain == null)
                return;

            Assemblies.Remove(targetDomain);
            AppDomain.Unload(targetDomain);
        }
    }
}
