using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Extensions.DependencyInjection;
using Akka.Routing;
using MarketMan.Celeb.Business;
using MarketMan.Celeb.Business.Core;
using MarketMan.Celeb.Business.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace MarketMan.Celeb.WepApi
{
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:58536")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod(); 
                    });
            });
                       
           
            services.AddSingleton<ActorSystem>(sp=> {
                var _actorSystem = ActorSystem.Create("engine");
                _actorSystem.UseServiceProvider(sp);
                return _actorSystem; 
            });

            services.AddSingleton<CelebJsonFileRepository, CelebJsonFileRepository>();
            services.AddSingleton<IScrapEngine, ImdbScrapEngine>();
            services.AddTransient<IRepository, ActorRepoImpl>();            
            services.AddTransient<RepositoryActor, RepositoryActor>();
            //services.AddSingleton<RepositoryActor, RepositoryActor>(sp => 
            //{
            //    var actorSystem = sp.GetService<ActorSystem>();                 
            //    return actorSystem.ActorOf(actorSystem.DI().Props<RepositoryActor>());
            //});

            //Akka.Routing.RoundRobinRoutingLogic rc = new RoundRobinRoutingLogic();
             


            services.AddSingleton<ActorProvider>(sp => 
            {
                var actorSystem = sp.GetService<ActorSystem>();                
                var repoActor = actorSystem.ActorOf(actorSystem.DI().Props<RepositoryActor>());                 
                return () => repoActor;               
            });
            
            services.AddControllers();
             

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
                        
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();
            
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices.GetService<IRepository>(); //create the singelton already so it will do initial loading
        }
    }
}
