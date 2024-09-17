using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SirclDocs.Website.Data;
using SirclDocs.Website.Logging;
using SirclDocs.Website.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            #region DataProtection

            services.AddDataProtection()
                .PersistKeysToDbContext<ApplicationDbContext>();

            #endregion

            #region Cors

            services.AddCors(options =>
            {
                options.AddPolicy("SamplesPolicy",
                    builder => {
                        builder.WithOrigins("https://cdpn.io")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                        //builder.AllowAnyOrigin()
                        //.AllowAnyHeader()
                        //.AllowAnyMethod();
                    });
            });

            #endregion

            #region Content

            services.AddDbContext<Data.Content.ContentDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            #endregion

            #region Logging

            services.AddDbContext<Data.Logging.LoggingDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<SirclDocs.Website.Logging.RequestLogger>();

            #endregion

            #region Samples

            services.AddDbContext<Data.Samples.SamplesDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            #endregion

            #region SMTP
            
            services.AddTransient<IEmailSender, SmtpEmailSender>(i =>
                new SmtpEmailSender(
                    Configuration["SmtpEmailSender:Host"],
                    Configuration.GetValue<int>("SmtpEmailSender:Port"),
                    Configuration.GetValue<bool>("SmtpEmailSender:EnableSSL"),
                    Configuration["SmtpEmailSender:UserName"],
                    Configuration["no-reply@getsircl.com"] // Retrieved from appsettings, Secrets, Environment Variables,...
                )
            );

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            #region Logging
            app.UseArebisRequestLog()
                .LogSlowRequests()
                .LogExceptions()
                .LogNotFounds();
            #endregion

            // Redirect "*://getsircl.com" to "*://www.getsircl.com":
            // https://weblog.west-wind.com/posts/2020/Mar/13/Back-to-Basics-Rewriting-a-URL-in-ASPNET-Core
            app.Use(async (context, next) =>
            {
                // Redirect to an external URL
                if (context.Request.Host.Host.Equals("getsircl.com", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.Redirect("https://www.getsircl.com" + context.Request.Path.Value);
                    return;   // short circuit
                }

                await next();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapRazorPages();

                #region Content

                endpoints.MapControllerRoute(
                    name: "content",
                    pattern: "{**path}",
                    defaults: new { controller = "Content", action = "Render" }
                );

                #endregion
            });
        }
    }
}
