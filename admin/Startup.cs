using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Entities.Context;
//using admin.Services.Abstract;
//using admin.Services.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using admin.Services.Abstract;
using admin.Services.Concrete.EntityFramework;
using Entities.Context;

namespace admin
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddControllersWithViews(x => x.SuppressAsyncSuffixInActionNames = false)
                    .AddRazorRuntimeCompilation();

            services.AddTransient<IAdminService, EfAdminService>();
            services.AddTransient<IMediaService, EfMediaService>();

            services.AddTransient<ICategoryService, EfCategoryService>();
            services.AddTransient<IAuthorService, EfAuthorService>();
            services.AddTransient<IBookService, EfBookService>();
            services.AddTransient<IStaffService, EfStaffService>();
            services.AddTransient<IMemberService, EfMemberService>();
            services.AddTransient<IMailMessageService, EfMailMessageService>();
            services.AddTransient<IProcessService, EfProcessService>();
            services.AddTransient<IMulctService, EFMulctService>();
            services.AddTransient<IToDoListService, EfToDoListService>();

            services.AddTransient<IUnitOfWork, EfUnitOfWork>();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.LoginPath = "/home/login";
              });


            //Role Services
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Editor", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "editor");
                });

                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "admin");
                });
                options.AddPolicy("Member", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "member");
                });
            });


            if (Environment.IsDevelopment())
            {
                services.AddControllersWithViews(x => x.SuppressAsyncSuffixInActionNames = false)
                .AddRazorRuntimeCompilation();

                services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Development")));

            }
            else
            {
                services.AddControllersWithViews();

                services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Production")));
            }
            services.AddMvc(options => options.EnableEndpointRouting = false);


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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
