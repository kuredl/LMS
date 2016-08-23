using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace LMS_Server {
    using System.IO;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public static class LogUtilities {
        static ConsoleColor _default = ConsoleColor.Black;
        public static void LogStatusCode(int status) {
            if (status == 200)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("Response : " + status);
            Console.ForegroundColor = _default;
        }
    }
    class Program
    {
        
        static void Main(string[] args) {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("Katana server booting up:");
            Console.WriteLine("----------------------------------------");
            var options = new StartOptions("http://*:8080") {
                ServerFactory = "Microsoft.Owin.Host.HttpListener"
            };
            if (args.Contains("-local")){
                options.Urls[0] = "http://*:8080";
            }
            using (WebApp.Start<Startup>(options)){
                Console.WriteLine("Started Server on: " +options.Urls[0]);
                while (true) {
                    ConsoleKey input = Console.ReadKey().Key;
                    if (input == ConsoleKey.Escape) {
                        break;
                    }
                }
                
                Console.WriteLine("Stopping Server!");
                Console.ReadKey();
            }
        }

    }


    public static class AppBuilderExtensions {
        public static void UseHelloWorld(this IAppBuilder app) {
            app.Use<HelloWorldComponent>();
        }
    }

    public class HelloWorldComponent {
        AppFunc _next;
        public HelloWorldComponent(AppFunc next){
            _next = next;
        }
        public Task Invoke(IDictionary<string, object> environment) {
            var response = environment["owin.ResponseBody"] as Stream;
            using (var writer = new StreamWriter(response)) {
                return writer.WriteAsync("Helloooo!!!");
            }
        }
    }
}
