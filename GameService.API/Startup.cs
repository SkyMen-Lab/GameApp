using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GameApp.Services;
using GameService.Domain.Configs;
using GameService.Domain.Models;
using GameService.Domain.Repositories;
using GameService.TCP;
using GameService.TCP.EventHandling;
using GameService.TCP.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GameApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DbSettings>(
                Configuration.GetSection("DebDbSettings"));
            services.Configure<TcpSettings>(
                Configuration.GetSection("TcpSettings"));
            services.AddSingleton<IDbSettings>(provider =>
                provider.GetRequiredService<IOptions<DbSettings>>().Value);
            services.AddSingleton<ITcpSettings>(provider =>
                provider.GetRequiredService<IOptions<TcpSettings>>().Value); 
            services.AddSingleton<MongoRepository>();
            services.AddSingleton<IEventManager, EventManager>();
            services.AddControllers();
            services.AddSingleton<ITcpManager, TcpManager>();
            services.AddSingleton<IGameManager, GameManager>();
            services.AddHostedService<TcpService>();
            services.AddMvc()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}