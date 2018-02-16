using Makro.Core.Webservice;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Makro.Core.Webservice
{
    using System.Web.Http;
    using Microsoft.Owin;
    using Microsoft.Owin.Extensions;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;
    using System.Web.Http.Dependencies;
    using Implementation;
    using Unity;
    using System.Web.Http.Dispatcher;
    using System.Reflection;
    using System.Collections.Generic;
    public class Startup
    {
        #region Custom Implementation
        protected static IList<Assembly> assemblies;

        //public Startup()
        //{
        //    assemblies = new List<Assembly>();
        //}

        public static void AddAssembly(Assembly aiAssembly)
        {
            if (assemblies == null)
                assemblies = new List<Assembly>();
            assemblies.Add(aiAssembly);
        }
        #endregion

        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            IUnityContainer container = new UnityContainer();
            httpConfiguration.DependencyResolver = new DependecyResolver(container);

            DynamicAssemblyResolver assemblyResolver = new DynamicAssemblyResolver(assemblies);
            httpConfiguration.Services.Replace(typeof(IAssembliesResolver), assemblyResolver);

            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(httpConfiguration);
        }
    }
}
