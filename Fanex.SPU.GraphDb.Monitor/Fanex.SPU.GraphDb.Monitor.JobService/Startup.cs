namespace Fanex.SPU.GraphDb.Monitor.JobService
{
    using Fanex.Data;
    using Fanex.Data.MySql;
    using Fanex.Logging;
    using Fanex.Logging.Sentry;
    using Fanex.SPU.GraphDb.Data;
    using Fanex.SPU.GraphDb.Data.Neo4j;
    using Fanex.SPU.GraphDb.Monitor.JobService.Services;
    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Sentry;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = new AppSettings(Configuration);
            services.AddSingleton<IAppSettings>(appSettings);


            services.AddControllers();
            services.AddHealthChecks();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fanex.SPU.GraphDb.Monitor", Version = "v1" });
            });

            DbSessionFactory.Init()
                .RegisterConfigs(Configuration, "GraphDB")
                .RegisterXmlFile("~/App_Data/Graph", "Script.xml")
                .RegisterDriverFactory(new Neo4jDriverFactory())
                .RegisterCommandBuilder(new Neo4jCommandBuilder("~/App_Data/Graph/scripts", "cypher"))
                .RegisterCommandExecutor(new Neo4jExecutor())
                .Run();

            services.RegisterService();

            LogManager
                .SetDefaultLogCategory("")
                .Use(new List<ILogEngine>
                {
                     new SentryLogging(new SentryEngineOptions {
                        Dsn = new Dsn(appSettings.SentryDsn),
                        Environment = "DEV"
                     })
                });

            ConfigFanexData(appSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fanex.SPU.GraphDb.Monitor v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigFanexData(IAppSettings appSettings)
        {
            var connections = Configuration
                                  .GetSection("Connections")
                                  .GetChildren()
                                  .AsEnumerable()
                                  .ToDictionary(
                                           x => x.GetValue<string>("Name"),
                                           y => new ConnectionConfiguration(y.GetValue<string>("Name"), y.GetValue<string>("ConnectionString"), y.GetValue<string>("ProviderName"))
                                   );

            DbSettingProviderManager
              .StartNewSession()
              .Use(connections)
              .WithMySql()
              .UseDefaultDbSettingProvider(resourcePath: appSettings.SPName)
              .Run();
        }
    }
}
