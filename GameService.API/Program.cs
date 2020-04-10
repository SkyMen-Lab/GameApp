using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GameApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Starting up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((host, config) =>
                {
                    var env = host.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                })
                .UseSerilog((context, configuration) =>
                {
                    var date = DateTime.Today;
                    configuration.Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.File($"logs/log_{date.Day}_{date.Month}_{date.Year}.log");
                })
                .Build();
    }
}