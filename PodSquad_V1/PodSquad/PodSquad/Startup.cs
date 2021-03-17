using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using PodSquad.Models;
using Microsoft.AspNetCore.Identity;
using PodSquad.Repositories;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using FluentSpotifyApi;
using FluentSpotifyApi.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.ClientCredentials;
using FluentSpotifyApi.AuthorizationFlows.ClientCredentials.DependencyInjection;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;

namespace PodSquad
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
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            // if statement to support azure db
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                services.AddDbContext<PodContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:AzureSQLServerConnection"]));
            }
            else
            {
                services.AddDbContext<PodContext>(options => options.UseSqlite(Configuration["ConnectionStrings:SQLiteConnection"]));
            }

            // identity service
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<PodContext>().AddDefaultTokenProviders();

            // inject repository into controllers
            services.AddTransient<IPodcastRepository, PodcastRepository>();
            services.AddTransient<IForumRepository, ForumRepository>();

            // spotify services
            services.AddFluentSpotifyClient();
            services.Configure<SpotifyClientCredentialsFlowOptions>(Configuration.GetSection("SpotifyClientCredentialsFlow"));
            services.AddFluentSpotifyClient().ConfigureHttpClientBuilder(b => b.AddSpotifyClientCredentialsFlow());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PodContext context)
        {
            // mitigation: x-frame-options header not set & x-content-type-options header missing
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                ctx.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
            });

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // enable CORS
            app.UseCors();

            // mitigation: cookie without secure flag
            app.UseCookiePolicy(new CookiePolicyOptions { HttpOnly = HttpOnlyPolicy.Always, Secure = CookieSecurePolicy.Always });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            var serviceProvider = app.ApplicationServices;
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var repo = serviceProvider.GetRequiredService<IPodcastRepository>();

            SeedData.Seed(context, roleManager, userManager, repo);
        }
    }
}
