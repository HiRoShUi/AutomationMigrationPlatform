namespace Makro.Core.Webservice.Controllers
{
    using System;
    using System.Web.Http;

    public class HealthCheckController : ApiController
    {
        public IHttpActionResult GetValues()
        {
            return Ok(new[] { "Server is up and running on " + Environment.MachineName });
        }
    }
}
