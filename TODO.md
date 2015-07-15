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
    <li>
        Set-up error handling and error pages. See <a href="https://github.com/aspnet/Diagnostics/blob/dev/samples/StatusCodePagesSample/Startup.cs">Status Code Error Pages</a>.
        <pre>
        // Web API does not need full error pages:
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
    <li>Set-up configurationBuilder.AddUserSecrets(); and userSecretsId in project.json correctly.</li>
    <li>Create new tag helper versions of Open Graph and Twitter code.</li>
    <li>Look into <a href="https://github.com/aspnet/Localization/blob/1.0.0-beta5/samples/LocalizationSample/Startup.cs">localization</a>.</li>
    <li>Compile LESS or SASS to CSS based on file extension in Gulpfile.js.</li>
    <li>
        Consider adding "Microsoft.AspNet.Session": "1.0.0-beta5"
        <pre>
        app.UseSession();
        this.Context.Session.GetInt("UserID");
        this.Context.Session.GetString("UserName");
        // Helper Extension Methods
        public static DateTime? GetDateTime(this ISessionCollection collection, string key)
        {
            var data = collection.Get(key);
            if(data == null)
            {
                return null;
            }

            long dateInt = BitConverter.ToInt64(data, 0);
            return new DateTime(dateInt);
        }
        public static void SetDateTime(this ISessionCollection collection, string key, DateTime value)
        {
            collection.Set(key, BitConverter.GetBytes(value.Ticks));
        }
        </pre>
    </li>
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
    <li>Update the ReadMe.html for MVC 6.</li>
</ul>
<p>Wait for Microsoft to finish MVC 6 before adding:</p>
<ul>
    <li>Add "releaseNotes": "Initial Release", and 'Require License Acceptance' to project.json in Boilerplate.Web.Mvc6.</li>
    <li>CacheProfile.VaryByParam in Startup.CacheProfiles.cs</li>
    <li>System.ServiceModel.SyndicationFeed. See <a href="https://github.com/dotnet/wcf/issues/76#issuecomment-111420491">GitHub</a></li>
    <li>RouteOptions.AppendTrailingSlash in Startup.Routing.cs. See <a href="http://stackoverflow.com/questions/27997814/lower-case-urls-and-trailing-slash/30799844#30799844">StackOverflow</a> and <a href="https://github.com/aspnet/Mvc/issues/2691">GitHub</a></li>
    <li>Support for signing Core CLR assemblies. See <a href="http://stackoverflow.com/questions/31001880/signing-asp-net-5-class-library-assemblies/31259763#31259763">StackOverflow</a>.</li>
    <li>Add <a href="https://github.com/aspnet/StaticFiles/issues/28">missing MIME types</a> if Microsoft does not add them.</li>
    <li>Change Boilerplate.Web.Mvc5 to use an ASP.NET 5 class library project.</li>
</ul>
<p>Wait for third party libraries to support MVC 6:</p>
<ul>
    <li>NWebSec</li>
    <li>Elmah</li>
    <li>Glimpse</li>
</ul>

<h2>ASP.NET MVC 6 Web API</h2>
<p>A Web API Controller for ASP.NET MVC 6.</p>
<ul>
  <li>Add support for application/json-patch+json using the built-in <a href="https://github.com/aspnet/Mvc/issues/1976">stuff</a>.</li>
  <li>Use <a href="https://github.com/mikekelly/hal_specification/blob/master/hal_specification.md">HAL</a> or <a href="https://github.com/kevinswiber/siren">SIREN</a>. See also <a href="http://phlyrestfully.readthedocs.org/en/latest/halprimer.html">this</a> and <a href="https://msdn.microsoft.com/en-us/magazine/jj883957.aspx">this</a> and <a href="https://github.com/JakeGinnivan/WebApi.Hal">this</a>.</li>
</ul>

<h2>ASP.NET Identity</h2>
<p>A new project template including the ASP.NET Identity package out of the box.</p>
<ul>
  <li>ASP.NET Identity Project Template.</li>
  <li>Use a better (slower) encryption algorithm like bcrypt.net, zetetic.security, scrypt (See <a href="http://blog.codinghorror.com/your-password-is-too-damn-short/">this</a> for more information.</li>
</ul>
