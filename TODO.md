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
    <li>Add Mocha JavaScript unit testing.</li>
    <li>Add <a href="https://github.com/postcss/autoprefixer">autoprefixer</a> support for CSS.</li>
    <li>Test 404 Not Found error page when navigating to /this-resource-does-not-exist. See <a href="http://stackoverflow.com/questions/31606521/displaying-a-404-not-found-page-for-asp-net-5-mvc-6">this</a>.</li>
    <li>Build a HttpException for MVC 6. See <a href="http://stackoverflow.com/questions/31054012/asp-net-5-mvc-6-equivalent-of-httpexception">here</a>.</li>
    <li>Create new tag helper versions of Open Graph and Twitter code.</li>
    <li>
        Implement localization. See <a href="https://github.com/aspnet/Localization/blob/1.0.0-beta5/samples/LocalizationSample/Startup.cs">GitHub</a> and <a href="http://stackoverflow.com/questions/31721395/mvc-6-how-to-use-resx-files/31722153?noredirect=1">StackOverflow</a>
        <pre>
            // services.AddMvcLocalization();

            // application.UseCultureReplacer();
            // application.UseRequestLocalization();
        </pre>
    </li>
    <li>
        Implement CORS.
        <pre>
            // services.ConfigureCors(
            //     corsOptions =>
            //     {
            //         // TODO
            //     });
        </pre>
    </li>
    <li>
        Implement formatters.

        // TODO: Add the BSON input and output formatters.
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
</ul>
<p>Wait for Microsoft to finish MVC 6 before adding:</p>
<ul>
    <li>Sign Boilerplate.Web.Mvc6 with "keyFile": "../../Key.snk"</li>
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
</ul>

<h2>ASP.NET MVC 6 Web API</h2>
<p>A Web API Controller for ASP.NET MVC 6.</p>
<ul>
    <li>
        Implement Web API
        <pre>
            // Lets you pass a format parameter into the query string to set the response type e.g. ?format=json
            [FormatFilter]
            public class WebApiController : Controller { }
            
            // Add Web API to the request pipeline.
            ConfigureApi(application);
            
            using Microsoft.AspNet.Builder;
            using Microsoft.AspNet.Hosting;
            public partial class Startup
            {
                private const string ApiPath = "/api";
            
                /// &lt;summary&gt;
                /// Adds Web API to the request pipeline.
                /// &lt;/summary&gt;
                /// &lt;param name="application">The application.&lt;/param&gt;
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

            // Force 204 No Content response, when returning null values.
            // mvcOptions.OutputFormatters.Insert(0, new HttpNoContentOutputFormatter());

            // Force 406 Not Acceptable responses if the media type is not supported, instead of returning the default.
            // mvcOptions.OutputFormatters.Insert(0, new HttpNotAcceptableOutputFormatter());
        </pre>
    </li>
    <li>Add support for application/json-patch+json using the built-in <a href="https://github.com/aspnet/Mvc/issues/1976">stuff</a>.</li>
    <li>Use <a href="https://github.com/mikekelly/hal_specification/blob/master/hal_specification.md">HAL</a> or <a href="https://github.com/kevinswiber/siren">SIREN</a>. See also <a href="http://phlyrestfully.readthedocs.org/en/latest/halprimer.html">this</a> and <a href="https://msdn.microsoft.com/en-us/magazine/jj883957.aspx">this</a> and <a href="https://github.com/JakeGinnivan/WebApi.Hal">this</a>.</li>
</ul>

<h2>ASP.NET Identity</h2>
<p>A new project template including the ASP.NET Identity package out of the box.</p>
<ul>
  <li>ASP.NET Identity Project Template.</li>
  <li>Use a better (slower) encryption algorithm like bcrypt.net, zetetic.security, scrypt (See <a href="http://blog.codinghorror.com/your-password-is-too-damn-short/">this</a> for more information.</li>
</ul>
