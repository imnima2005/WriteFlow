using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WriteFlow.DataLayer.Context;
using WriteFlow.CoreLayer.Services.Users;
using WriteFlow.CoreLayer.Services.Categories;
using WriteFlow.CoreLayer.Services.Posts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.Services.FileManager;
using WriteFlow.CoreLayer.Services.Comments;
using WriteFlow.CoreLayer.Services.MainPage;

namespace WriteFlow
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
            services.AddRazorPages(options=> { });
            services.AddControllersWithViews();
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IPostServices, PostServices>();
            services.AddScoped<ICommentServices, CommentServices>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IMainPageServices, MainPageServices>();
            services.AddDbContext<WriteFlowContext>( option => {
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddAuthorization(option =>
            {
                option.AddPolicy("AdminPolicy", builder =>
                {
                    builder.RequireRole("Admin");
                });
            });
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(option =>
            {
                option.LoginPath = "/Authentication/Login";
                option.LogoutPath = "/Authentication/Login";
                option.ExpireTimeSpan = TimeSpan.FromDays(30);
                option.AccessDeniedPath = "/";
            });
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
                app.UseExceptionHandler("/ErrorHandler/500");
                
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/ErrorHandler/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "AdminArea",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}"
                );

                endpoints.MapRazorPages();
            });
        }
    }
}
