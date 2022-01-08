using ASP.net_Aplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ASP.net_Aplication {
    public class Startup {
        public Startup(IConfiguration configuration) {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();
            //session
            services.AddSession();
            //database
            services.AddDbContext<DB>(o => o.UseSqlServer(this.Configuration["DataBase:Connect"]));
            services.AddTransient<IRep, Rep>();
            //identity
            //services.AddDbContext<DB>(o => o.UseSqlServer(Configuration["DataBase:Connect"]));
            services.AddIdentity<ModelAccount, IdentityRole>()
                .AddEntityFrameworkStores<DB>()
                .AddDefaultTokenProviders();
            //Authorization
            services.AddAuthorization(o => {
                foreach (Role r in (Role[])Enum.GetValues(typeof(Role))) {
                    o.AddPolicy(r.ToString(), p => p.RequireRole(r.ToString()));
                }
            });
            //models
            services.AddTransient<IImageRep, EFImageRep>();
            services.AddTransient<IRateRep, EFRateRep>();
            services.AddTransient<ICommentRep, EFCommentRep>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"));
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}