using System;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Owin.Diagnostics;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.ContentTypes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Cookies;
using LMS_Server.Providers;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(LMS_Server.Startup))]
namespace LMS_Server {
    
    public class Startup
    {
        private bool _debug;
        public Startup() {
            _debug = System.Diagnostics.Debugger.IsAttached;
        }
        public void Configuration(IAppBuilder app) {
            ConfigureFileServer(app);
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.Use(async (environment, next) => {
                Console.WriteLine("Request  : " + environment.Request.Path);
                await next();
                LogUtilities.LogStatusCode(environment.Response.StatusCode);
            });

            ConfigureOAuth(app);
            if (_debug)
                app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            
            ConfigureWebApi(app);
        }

        private void ConfigErrors(IAppBuilder app) {
            app.UseErrorPage(new ErrorPageOptions() {
                ShowEnvironment = true,
                ShowHeaders = true,
                ShowCookies = false,
                ShowSourceCode = true,
                ShowQuery = true,
                ShowExceptionDetails = true,
            });
        }

        private void ConfigureFileServer(IAppBuilder app) {
            PhysicalFileSystem contentFolder = 
                new PhysicalFileSystem(ServerConfig.FileServerPath);
            var contentOptions = new FileServerOptions {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = contentFolder,
                RequestPath = new PathString("")
            };
            contentOptions.DefaultFilesOptions.DefaultFileNames.Add("index.html");
            contentOptions.EnableDirectoryBrowsing = false;
            app.UseFileServer(contentOptions);
        }

        private void ConfigureWebApi(IAppBuilder app) {
            var config = new HttpConfiguration();
            // Auth
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Routing
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });           
            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            RegisterCorsConfig(config);
            app.UseWebApi(config);
        }
        private void RegisterCorsConfig(HttpConfiguration config) {
            var cors = new EnableCorsAttribute("*", "*", "*", "*");
            cors.ExposedHeaders.Add("Content-Disposition");
            config.EnableCors(cors);
        }
        private void ConfigureOAuth(IAppBuilder app) {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions() {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
