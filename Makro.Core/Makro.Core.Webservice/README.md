This project is a default project for hosting a rest service with OWIN. It is based on the "OWIN ASP.NET WebAPI SPA Template" by Oliver Lohmann - MSFT in Version 1.3.

If you are using this project / library you are able to ingest ApiController at runtime into the webservice.

Example of use:

```csharp
			Makro.Core.Webservice.Implementation.Webservice service = new Webservice.Implementation.Webservice();
            var assembly = Assembly.LoadFrom("<dll-file where a few controllers are implemented with the basic-class 'IBasicController'>");
            service.AddControllerAssembly(assembly);
            service.Start(Webservice.Implementation.Enums.Protocol.http,"localhost",5112);
```

			This would start a webservice running with OWIN on port 5112 on the localhost for http. The default webservice would only contain a HealthCheck-Controller under resource "http://localhost:5112/api/HealthCheck" which returns a HttpStatusCode.Ok if a get has been performed.
			
			A example of a controller that is injected at runtime would be something like: 

		```csharp
			 public class TestController: IBasicController
			{
				public IHttpActionResult GetValues()
				{
					return Ok(new[] { "Testcontroller is up and running on " + Environment.MachineName });
				}
			}
			```

(C) by MaKro