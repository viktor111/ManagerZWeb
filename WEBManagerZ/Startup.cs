using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Data;
using WEBManagerZ.Models;
using WEBManagerZ.Services;

namespace WEBManagerZ
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
            services.AddDbContext<ManagerZContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("Ip")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddRazorPages();

            services.AddDefaultIdentity<AppUser>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ManagerZContext>();

            services.AddControllersWithViews();

            services.AddScoped<SqlProduct>();
            services.AddScoped<SqlCart>();
            services.AddScoped<SqlOrder>();
            services.AddScoped<SqlDiscount>();
        }
   

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //CreateRoles(serviceProvider).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        //private async Task CreateRoles(IServiceProvider serviceProvider)
        //{
        //    var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //    var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        //    var SqlCart = serviceProvider.GetRequiredService<SqlCart>();

        //    string adminRole = "Admin";

        //    var roleExist = await RoleManager.RoleExistsAsync(adminRole);

        //    if (!roleExist)
        //    {
        //        await RoleManager.CreateAsync(new IdentityRole(adminRole));
        //    }

        //    var poweruser = new AppUser
        //    {
        //        UserName = "dragataAdminWorker",
        //        Email = "swifrorlilko@gmail.com",
        //    };

        //    string userPWD = "adminworker434";

        //    var _user = await UserManager.FindByEmailAsync("swifrorlilko@gmail.com");

        //    if (_user == null)
        //    {
        //        SqlCart.CreateCart(poweruser);
        //        var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
               
        //        if (createPowerUser.Succeeded)
        //        {
        //            await UserManager.AddToRoleAsync(poweruser, adminRole);
        //        }
        //    }
        //}
    }
}
