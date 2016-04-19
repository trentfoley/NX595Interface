using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;

namespace NX595Interface
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var appSettingsJsonFile = "appsettings.json";

            // Set up configuration sources.
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(appSettingsJsonFile)
                .AddEnvironmentVariables()
                .Build()
                .ReloadOnChanged(appSettingsJsonFile);
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup options with DI
            services.AddOptions();

            // Configure AppSettings using config
            services.Configure<AppSettings>(Configuration);

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseDefaultHostingConfiguration(args)
                .UseKestrel()
                .CaptureStartupErrors(true)
                .UseStartup<Startup>()
                .Build();
               
            host.Run();
        }
    }
}
