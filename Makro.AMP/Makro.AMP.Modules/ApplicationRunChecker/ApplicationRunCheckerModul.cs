using Makro.Core.ModuleSystem.Implementation;
using Makro.Core.Serialization.Implementation;
using Makro.Core.Settings.Implementation;
using Microsoft.Xrm.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Makro.AMP.Modules.ApplicationRunChecker
{
    [Serializable]
    public class ApplicationRunCheckerModul : BaseTimerBackgroundWorker
    {
        public override object FunctionImplementation()
        {
            CheckIfServicesAvailable();
            return null;
        }

        private void CheckIfServicesAvailable()
        {
            bool runingSuccessful = true;

            var settings = new JsonFileSettings("settings.json", new NewtonsoftJsonSerializer());
            settings.LoadSettingProperties();

            //CheckCRM
            if (!CheckCRMConnection(settings.RetrieveProperty("Crm Connection").Value.ToString()))
                runingSuccessful = false;

            if (runingSuccessful)
            {
                Console.WriteLine("Everything worked as expected and all services will be started.");
                return;
            }

            Console.WriteLine("One or more errors occured. AWA-Service will be shuted down in 10 seconds.");
            Thread.Sleep(10000);
            Environment.Exit(0);
        }



        /// <summary>
        /// Checks if the CRM is available under the specified URL.
        /// </summary>
        /// <param name="aiConnectionURL"></param>
        /// <returns></returns>
        private bool CheckCRMConnection(string aiConnectionURL)
        {
            try
            {
                Console.WriteLine("Try to connect to the CRM under the URL '" + aiConnectionURL + "'.");
                var connection = CrmConnection.Parse(aiConnectionURL);
                if (connection != null)
                {
                    Console.WriteLine("Connection to the CRM was successful.");
                    return true;
                }
            }
            catch { }
            Console.WriteLine("Connection to the CRM was NOT successful!");
            return false;
        }

    }
}
