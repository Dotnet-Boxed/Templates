![.NET Boxed Banner](https://raw.githubusercontent.com/Dotnet-Boxed/Templates/master/Images/Banner.png)

![ASP.NET Core GraphQL Boxed Preview Image](https://raw.githubusercontent.com/Dotnet-Boxed/Templates/master/Images/GraphQL-Preview.png)

## Technology Map

The ASP.NET Core GraphQL project template contains the following features:

![ASP.NET Core GraphQL Boxed Technology Map](https://raw.githubusercontent.com/Dotnet-Boxed/Templates/master/Images/GraphQL-Technology-Map.png)

## Optional Feature Selection

The ASP.NET Core GraphQL project template uses `dotnet new` to enable you to turn features of the project template on or off. Literally everything can be turned on or off with the click of a button for a truly personalized project. Find out more about `dotnet new` [here](http://rehansaeed.com/custom-project-templates-using-dotnet-new/).

#### GraphQL

- **Mutations** (Default=On) - Add [GraphQL](http://graphql.org/) mutations to change objects.
- **Subscriptions** (Default=On) - Add [GraphQL](http://graphql.org/) subscriptions to be notified when objects change.

#### Project

- **Title** - The name of the project which determines the assembly product name.
- **Description** - A description of the project which determines the assembly description.
- **Author** - The name of the author of the project which determines the assembly author and copyright information.

#### Security

- **HttpsEverywhere** (Default=On) - Use the HTTPS scheme and TLS security across the entire site, redirects HTTP to HTTPS and adds a Strict Transport Security (HSTS) HTTP header with preloading enabled. Configures the primary and reverse proxy web servers for best security and adds a development certificate file for use in your development environment.
- **CORS** (Default=On) - Browser security prevents a web page from making AJAX requests to another domain. This restriction is called the same-origin policy, and prevents a malicious site from reading sensitive data from another site. CORS is a W3C standard that allows a server to relax the same-origin policy. Using CORS, a server can explicitly allow some cross-origin requests while rejecting others.

#### Web Server

- **LoadBalancer** - If you use a load balancer, updates the request scheme using the X-Forwarded-Proto HTTP header.
- **ReverseProxyWebServer** - The internet facing reverse proxy web server you want to use in front of the primary web server to host the site.
  - **Internet Information Services (IIS)** - A flexible, secure and manageable Web server for hosting anything on the Web using Windows Server. Select this option if you are deploying your site to Azure web apps. IIS is preconfigured to set request limits for security.
  - **NGINX** - A free, open-source, cross-platform high-performance HTTP server and reverse proxy, as well as an IMAP/POP3 proxy server. It does have a Windows version but it's not very fast and IIS is better on that platform. If the HTTPS Everywhere feature is enabled, NGINX is pre-configured to enable the most secure TLS protocols and ciphers for security and to enable HTTP 2.0 and SSL stapling for performance.
  - **Both** (Default) - Support both reverse proxy web servers.

#### Analytics

- **CorrelationId** (Default=On) - Correlate requests between clients and this API. Pass a GUID in the X-Correlation-ID HTTP header to set the HttpContext.TraceIdentifier. The header is also reflected back in a response.
- **Analytics** - Monitor internal information about how your application is running, as well as external user information.
  - **Application Insights** - Monitor internal information about how your application is running, as well as external user information using the Microsoft Azure cloud.
  - **None** (Default) - Not using any analytics.
- **ApplicationInsightsKey** - Your Application Insights instrumentation key e.g. 11111111-2222-3333-4444-555555555555.

#### Cloud

- **CloudProvider** - Select which cloud provider you are using if any, to add cloud specific features.
  - **Azure** - The Microsoft Azure cloud. Adds logging features that let you see logs in the Azure portal.
  - **None** (Default) - No cloud provider is being used.

#### Other

- **robots.txt** (Default=On) - Adds a robots.txt file to tell search engines not to index this site.
- **humans.txt** (Default=On) - Adds a humans.txt file where you can tell the world who wrote the application. This file is a good place to thank your developers.

## Always On Features

#### GraphQL

- **Example Queries, Mutations, Subscriptions** - Provides an example schema based on the Star Wars movie.
- **Automatically Return Not Acceptable** - Returns a 406 Not Acceptable if the MIME type in the Accept HTTP header is not valid.

#### Performance

- **GZip Compression** - Turned on by default for static and dynamic files making them smaller and quicker to download. 
- **Caching Static Files** - Static files are cached by default using the `Cache-Control` HTTP header.
- **AddMvcCore** - Uses only the features and packages from ASP.NET Core required for [GraphQL](http://graphql.org/).

#### Security

- **User Secrets** - This feature is turned on when the site is in development mode to allow storing of secrets on your development machine.
- **Remove Server HTTP Header** - Removes the Server HTTP header for security and performance.

#### Logging

- **Serilog** - Has [Serilog](https://serilog.net/) built in for an excellent structured logging experience.

#### Debugging

- **Developer Exception Page** - Shows detailed exception information in the browser. Turned on in development mode only for security.

#### Patterns & Practices

- **Configure CacheProfile in JSON** - All cache profiles can be configured from a configuration file.

## How can I get it?

1. Install the latest [.NET Core SDK](https://dot.net).
2. Run `dotnet new --install "Boxed.Templates::*"` to install the project template.
3. Run `dotnet new api --help` to see how to select the feature of the project.
5. Run `dotnet new api --name "MyTemplate"` along with any other custom options to create a project from the template.

## Release Notes and To-Do List
You can find release notes for each version [here](https://github.com/Dotnet-Boxed/Templates/releases) and a To-Do list of new features and enhancements coming soon in the [projects](https://github.com/Dotnet-Boxed/Templates/projects) tab.

## Contributing

Please read the [guide](https://github.com/Dotnet-Boxed/Templates/blob/master/CONTRIBUTING.md) to learn how you can make a contribution.
