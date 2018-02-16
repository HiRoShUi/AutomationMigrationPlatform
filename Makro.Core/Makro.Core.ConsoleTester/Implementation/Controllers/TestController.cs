using Makro.Core.Webservice.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Makro.Core.ConsoleTester.Implementation.Controllers
{
    public class TestController: IBasicController
    {
        public IHttpActionResult GetValues()
        {
            return Ok(new[] { "Testcontroller is up and running on " + Environment.MachineName });
        }
    }
}
