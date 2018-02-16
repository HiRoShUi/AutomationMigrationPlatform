using Makro.Core.ModuleSystem.HelpClasses;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace Makro.Core.Webservice.Implementation
{
    internal class DependecyResolver : IDependencyResolver
    {
        protected IUnityContainer _container;

        public DependecyResolver(IUnityContainer aiContainer)
        {
            if (aiContainer == null)
            {
                throw new ArgumentNullException("container");
            }
            this._container = aiContainer;
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new DependecyResolver(child);
        }


        public void Dispose()
        {
            Dispose(true);
        }


        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            _container.Dispose();
        }
    }
}