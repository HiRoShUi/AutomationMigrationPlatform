using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.ModuleSystem.nsInterfaces
{
    public interface IExtensionProxy
    {
        List<AppDomain> Assemblies { get; }
        void Unload(string aiAssemblyName);

        object GetInstance(string aiAssemblyPath, string aiAssemblyName, string aiTypeName);
    }
}
