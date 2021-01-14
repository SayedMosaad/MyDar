using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using MyDar.Areas.Admin.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MyDar.Areas.Admin.Models.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyDar
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false) ;
            services.AddDbContext<ApplicationDBContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("SqlCon"));
            });
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();
            services.AddAuthentication();
            services.AddAuthorization();
            
            services.AddScoped<IApplicationRepository<Slider>, SliderRepository>();
            services.AddScoped<IApplicationRepository<Profile>, ProfileRepository>();
            services.AddScoped<IApplicationRepository<Services>, ServicesRepository>();
            services.AddScoped<IApplicationRepository<Features>, FeaturesRepository>();
            services.AddScoped<IApplicationRepository<Packages>, PackagesRepository>();
            services.AddScoped<IApplicationRepository<Testemonials>, TestemonialsRepository>();
            services.AddScoped<IApplicationRepository<Blogs>, BlogsRepository>();
            services.AddScoped<IApplicationRepository<Videos>, VideosRepository>();
            services.AddScoped<IApplicationRepository<Team>, TeamRepository>();
            services.AddScoped<IApplicationRepository<Categories>, CategoriesRepository>();
            services.AddScoped<IApplicationRepository<Projects>, ProjectsRepository>();
            services.AddScoped<IApplicationRepository<Photos>, ImagesRepository>();
            services.AddScoped<IimagesRepository, ImagesRepository>();
            services.AddScoped<IApplicationRepository<Request>, RequestRepository>();
            services.AddCloudscribePagination();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "MyAdminArea",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


            });
        }

    }
}
