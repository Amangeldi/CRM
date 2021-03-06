using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.BLL.Interfaces;
using CRM.BLL.Services;
using CRM.DAL.EF;
using CRM.DAL.Entities;
using CRM.RAZOR.Mappings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CRM.RAZOR
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

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddControllersWithViews();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApiContext>();
            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<ApiContext>();
            services.AddControllersWithViews();
            services.AddDbContext<ApiContext>(options =>
                options.UseSqlServer(connection));
            services.AddTransient(typeof(IUserRegistrationService), typeof(UserRegistrationService));
            services.AddTransient(typeof(IRegionService), typeof(RegionService));
            services.AddTransient(typeof(ICountryService), typeof(CountryService));
            services.AddTransient(typeof(IQualificationService), typeof(QualificationService));
            services.AddTransient(typeof(ICompanyService), typeof(CompanyService));
            services.AddTransient(typeof(IMailFindService), typeof(MailFindService));
            services.AddScoped<SelectedItemService>();
            services.AddTransient<CompanyMap>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddSignalR();
            services.AddSingleton<TempService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                //endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
