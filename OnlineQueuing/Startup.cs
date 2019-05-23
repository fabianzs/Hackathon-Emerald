using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineQueuing.Data;
using OnlineQueuing.Helpers;
using OnlineQueuing.Services;

namespace OnlineQueuing
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment env;

        public Startup(IHostingEnvironment environment)
        {
            this.env = environment;
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables();
            this.configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.AddDbContext<ApplicationContext>(builder =>
                       builder.UseInMemoryDatabase("InMemoryDatabase"));
          
            //services.AddDbContext<ApplicationContext>(builder =>
            //           builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            //            .EnableSensitiveDataLogging(true));

            services.AddAuthorization(auth =>
            {
            auth.AddPolicy("Admin", policy =>
                policy.Requirements.Add(new AdminRequirement()));
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders(); ;

            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                    })
                    .AddGoogle(options =>
                    {
                        options.ClientId = configuration["Authentication:Google:ClientId"];
                        options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                        options.AuthorizationEndpoint += "?prompt=consent";
                        options.AccessType = "offline";
                        options.SaveTokens = true;
                        options.Scope.Add("https://mail.google.com/");
                        options.Scope.Add("https://www.googleapis.com/auth/gmail.modify");
                        options.Scope.Add("https://www.googleapis.com/auth/gmail.compose");
                        options.Scope.Add("https://www.googleapis.com/auth/gmail.send");
                    });

            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();

        }
    }
}
