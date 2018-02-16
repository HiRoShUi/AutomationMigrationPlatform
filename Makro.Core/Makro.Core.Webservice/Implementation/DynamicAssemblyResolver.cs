using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Dispatcher;

namespace Makro.Core.Webservice.Implementation
{
    public class DynamicAssemblyResolver : DefaultAssembliesResolver
    {
        protected IList<Assembly> _assemblies;


        public DynamicAssemblyResolver(IList<Assembly> aiAssemblies)
        {
            _assemblies = aiAssemblies;
        }

        public void AddAssembly(Assembly aiAssembly)
        {
            if (_assemblies == null)
                _assemblies = new List<Assembly>();
            _assemblies.Add(aiAssembly);
        }

        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> baseAssemblies = base.GetAssemblies();
            List<Assembly> assemblies = new List<Assembly>(baseAssemblies);

            foreach (var entry in _assemblies)
            {
                var tmp = baseAssemblies.FirstOrDefault(ass => ass.FullName == entry.FullName);
                assemblies.Remove(tmp);
            }
            try
            {
                foreach (var assembly in assemblies)
                {
                    if (assembly != null)
                    {                    
                            assemblies.Add(assembly);
                    }
                }
            }
            catch
            {
                // We ignore errors and just continue
            }
            return assemblies;
        }
    }
}