using Makro.Core.Webservice.Implementation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Microsoft.Owin.Hosting;
using System.Reflection;

namespace Makro.Core.Webservice.Implementation
{
    public class Webservice
    {
        #region BasicControllers ToDo: ggf. nutzen falls sich es nicht über assemblies nutzbar ist.
        protected IList<Assembly> _assemblyControllers;
        public Webservice()
        {
            _assemblyControllers = new List<Assembly>();
        }
        #endregion

        public void Start(Protocol aiProtocol, string aiHost, int aiPort)
        {
            string baseAddress = aiProtocol.ToString() +  "://" + aiHost + ":" + aiPort.ToString() + "/";

            foreach (var ass in _assemblyControllers)
                Startup.AddAssembly(ass);
           
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();
                var response = client.GetAsync(baseAddress + "api/healthcheck").Result;//Check if api is accessible
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    Console.WriteLine("Everything seems running smoothly.");
                else
                    throw new Exception("HealthCheck ran wrong.");
                Console.ReadLine();
            }
        }

        public void AddControllerAssembly(Assembly aiAssembly)
        {
            _assemblyControllers.Add(aiAssembly);
        }
    }
}