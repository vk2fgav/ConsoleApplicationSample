using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.File;

using System;
using System.IO;

namespace ConsoleApplicationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Step #2 : add the ConfigurationBuilder
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            //Step #3 : Add the serilog
            //Sink File : https://github.com/serilog/serilog-sinks-file
            Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(builder.Build())
                            .Enrich.FromLogContext()
                            .WriteTo.Console()
                            .WriteTo.File("Serilog.txt", rollingInterval: RollingInterval.Day)                                                        
                            .CreateLogger();

            //Step #4 : Display the log to the console
            Log.Logger.Information("Application starting");

            //Step #5 :             
            var host = Host.CreateDefaultBuilder()
                        .ConfigureServices((context, services) => 
                        {
                            services.AddTransient<IGreetingService, GreetingService>();            
                        })
                        .UseSerilog()
                        .Build();

            var svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
            svc.Run();                          
        }

        //Step #1 : add this method
        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENIVRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }

    //Step #6 : Create the interface
    public interface IGreetingService
    {
        void Run();
    }

    //Step #7 : Add this entire class
    public class GreetingService : IGreetingService
    {
        private readonly ILogger<GreetingService> _log;
        private readonly IConfiguration _config;
        public GreetingService(ILogger<GreetingService> log, IConfiguration config)
        {
            _log = log;
            _config = config;     
        }

        public void Run()
        {
            //The value of LooTimes is saved in the appsettings.json file
            for(int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            {
                //Do not use the string interpolation because Serilog will serialize the object i with the name runNumber
                _log.LogInformation("Run number {runNumber}", i);
            }
        }

    }
}
