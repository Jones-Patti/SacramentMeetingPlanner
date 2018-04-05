using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Data;


namespace SacramentMeetingPlanner
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
            services.AddDbContext<SacramentMeetingPlannerContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

/*
dotnet ef dbcontext scaffold "Server=132.148.86.237;DataBase=SacramentMeetingPlanner;Uid=sacramentmeeting;Pwd=password" Pomelo.EntityFrameworkCore.MySql -o Models -f
 
dotnet aspnet-codegenerator controller -name HymnController -m Hymn -dc SacramentMeetingPlannerContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

dotnet aspnet-codegenerator controller -name MemberController -m People -dc SacramentMeetingPlannerContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

dotnet aspnet-codegenerator controller -name BishopricController -m Bishopric -dc SacramentMeetingPlannerContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

dotnet aspnet-codegenerator controller -name TopicController -m Topic -dc SacramentMeetingPlannerContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

dotnet aspnet-codegenerator controller -name SpeakerController -m Speaker -dc SacramentMeetingPlannerContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

*/