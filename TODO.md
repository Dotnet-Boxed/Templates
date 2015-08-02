<h1>TODO List</h1>
<p>A list of improvements and new features to be added. Feel free to submit your own.</p>

<h2>General Improvements</h2>
<p>Improvements that can be made to all project templates.</p>
<ul>
    <li>Make use of Object Pooling code. Add note about using Object Pooling in ReadMe.html.</li>
    <li>Add <a href="https://developers.google.com/structured-data/">Google Structured Data</a>.</li>
    <li>Keep eye on <a href="http://stackoverflow.com/questions/27860618/which-http-status-codes-to-cover-for-mvc-error-handling/29282406#29282406">HTTP Status Codes</a> for codes I am not covering correctly.</li>
    <li>Which HTTP handlers to remove <a href="http://stackoverflow.com/questions/28856991/removing-unused-http-handlers-for-better-performance-security">link</a>.</li>
    <li>Update to Font Awesome 4.3.0 when they fix the .less files.</li>
</ul>

<h2>ASP.NET MVC 6</h2>
<p>A new project template in addition to the existing ASP.NET MVC 5 template, targeting ASP.NET MVC 6.</p>
<ul>
    <li>Upgrade to Beta 6.</li>
    <li>Setup 404 Not Found error page when navigating to /this-resource-does-not-exist. See <a href="http://stackoverflow.com/questions/31606521/displaying-a-404-not-found-page-for-asp-net-5-mvc-6">this</a>.</li>
    <li>Build a HttpException for MVC 6. See <a href="http://stackoverflow.com/questions/31054012/asp-net-5-mvc-6-equivalent-of-httpexception">here</a>.</li>
    <li>Create new tag helper versions of Open Graph and Twitter code.</li>
    <li>
        <pre>
            // TODO: Write comments about localization.
            // services.AddMvcLocalization();


            // TODO
            //application.UseCultureReplacer();
            // application.UseRequestLocalization();
        </pre>
    </li>
    <li>
        <pre>
            // TODO: Write a comment about CORS.
            //services.ConfigureCors(
            //    corsOptions =>
            //    {
            //        // TODO
            //    });
        </pre>
    </li>
    <li>
        <pre>

            // Add Web API to the request pipeline.
            ConfigureApi(application);

    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;

    public partial class Startup
    {
        private const string ApiPath = "/api";

        /// <summary>
        /// Adds Web API to the request pipeline.
        /// </summary>
        /// <param name="application">The application.</param>
        private static void ConfigureApi(IApplicationBuilder application)
        {
            // A Web API does not need all of the features of MVC like custom error pages, CSP, Canonical URL, etc.
            // The Map method allows us to create a new clean application under the ApiPath without all of the above
            // features. Anyone navigating to /api will skip the MVC request pipeline and come straight here.
            application.Map(
                ApiPath, 
                app =>
                {
                    app.UseMvc();
                });
        }
    }

            // TODO: The Microsoft.AspNet.Mvc.Formatters.Xml package has gone missing. Add this back in when it comes back.
            // Add the XML input and output formatters. 
            // See: http://www.strathweb.com/2015/04/asp-net-mvc-6-formatters-xml-browser-requests/.
            // mvcOptions.AddXmlDataContractSerializerFormatter();

            // TODO: Add the BSON input and output formatters.

            // Force 204 No Content response, when returning null values.
            // mvcOptions.OutputFormatters.Insert(0, new HttpNoContentOutputFormatter());

            // Force 406 Not Acceptable responses if the media type is not supported, instead of returning the default.
            // mvcOptions.OutputFormatters.Insert(0, new HttpNotAcceptableOutputFormatter());
        </pre>
    </li>
    <li>Look into <a href="https://github.com/aspnet/Localization/blob/1.0.0-beta5/samples/LocalizationSample/Startup.cs">localization</a>. See <a href="http://stackoverflow.com/questions/31721395/mvc-6-how-to-use-resx-files/31722153?noredirect=1#comment51385769_31722153">here</a>.</li>
    <li>
        Consider adding an exception filter while there is no Elmah support.
        <pre>
        public class AppExceptionFilterAttribute : ExceptionFilterAttribute
        {
	        private readonly ILogger _logger;
            public AppExceptionFilterAttribute(ILoggerFactory loggerfactory)
            {
               _logger = loggerFactory.CreateLogger<AppExceptionFilterAttribute>();
            }
            public override void OnException(ExceptionContext context)
            {
                logger.WriteError(2, "Error Occurred", context.Exception);
                context.Result = new JsonResult(
                    new
                    {
                        context.Exception.Message,
                        context.Exception.StackTrace
                    });
            }
        }
        //Register your filter as a service (Note this filter need not be an attribute as such)
        services.AddTransient<AppExceptionFilterAttribute>();
        services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add(new AppExceptionFilterAttribute());
        });
        //On the controller/action where you want to apply this filter,
        //decorate them like
        [ServiceFilter(typeof(AppExceptionFilterAttribute))]
        public class HomeController : Controller
        {
            //....
        }
        </pre>
    </li>
    <li>Update the ReadMe.html for MVC 6.</li>
</ul>
<p>Wait for Microsoft to finish MVC 6 before adding:</p>
<ul>
    <li>CacheProfile.VaryByParam in Startup.CacheProfiles.cs</li>
    <li>System.ServiceModel.SyndicationFeed. See <a href="https://github.com/dotnet/wcf/issues/76#issuecomment-111420491">GitHub</a></li>
    <li>Change Boilerplate.Web.Mvc5 to use an ASP.NET 5 class library project.</li>
</ul>
<p>Wait for third party libraries to support MVC 6:</p>
<ul>
    <li>NWebSec</li>
    <li>Elmah</li>
    <li>Glimpse</li>
</ul>
<p>Contributions to ASP.NET 5 MVC 6 I am making to make it better.</p>
<ul>
    <li><a href="http://stackoverflow.com/questions/31458950/is-there-any-guidance-for-caching-patterns-in-asp-net-5/31513464#31513464">CacheExtensions</a> and <a href="https://github.com/aspnet/Caching/issues/79">GitHub</a></li>
    <li><a href="https://github.com/aspnet/Diagnostics/issues/144#issuecomment-125980689">UseStatusCodePagesWithReExecute</a></li>
</ul>

<h2>ASP.NET MVC 6 Web API</h2>
<p>A Web API Controller for ASP.NET MVC 6.</p>
<ul>
    <li>
        Consider creating an example WebApiController.
        <pre>
        //  ? format = json
        [FormatFilter]
        public class WebApiController : Controller
        {

        }
        </pre>
    </li>
    <li>
        <pre>
        app.Map("/api", ConfigureApi);
        private void ConfigureApi(IApplicationBuilder app)
		{
			app.Run(async (context) =>
			{
				await context.Response.WriteAsync("Hello World from API!");
			});
		}
        </pre>
    </li>
    <li>Add support for application/json-patch+json using the built-in <a href="https://github.com/aspnet/Mvc/issues/1976">stuff</a>.</li>
    <li>Use <a href="https://github.com/mikekelly/hal_specification/blob/master/hal_specification.md">HAL</a> or <a href="https://github.com/kevinswiber/siren">SIREN</a>. See also <a href="http://phlyrestfully.readthedocs.org/en/latest/halprimer.html">this</a> and <a href="https://msdn.microsoft.com/en-us/magazine/jj883957.aspx">this</a> and <a href="https://github.com/JakeGinnivan/WebApi.Hal">this</a>.</li>
    <li>
        Web API does not need full error pages.
        <pre>
        public class Startup
        {
	        public void Configure(IApplicationBuilder app)
	        {
		        app.Map("/api", ConfigureApi);

		        app.Run(async (context) =>
		        {
			        await context.Response.WriteAsync("Hello World!");
		        });
	        }

	        private void ConfigureApi(IApplicationBuilder app)
	        {
		        app.Run(async (context) =>
		        {
			        await context.Response.WriteAsync("Hello World from API!");
		        });
	        }
        }
        </pre>
    </li>
</ul>

<h2>ASP.NET Identity</h2>
<p>A new project template including the ASP.NET Identity package out of the box.</p>
<ul>
  <li>ASP.NET Identity Project Template.</li>
  <li>Use a better (slower) encryption algorithm like bcrypt.net, zetetic.security, scrypt (See <a href="http://blog.codinghorror.com/your-password-is-too-damn-short/">this</a> for more information.</li>
</ul>
