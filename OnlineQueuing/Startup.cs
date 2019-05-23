using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineQueuing.Data;
using OnlineQueuing.Seed;
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

            if (env.IsDevelopment())
            {
                services.AddDbContext<ApplicationContext>(builder =>
                        builder.UseInMemoryDatabase("InMemoryDatabase"));
            }
            if (env.IsProduction())
            {
                services.AddDbContext<ApplicationContext>(builder =>
                       builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                        .EnableSensitiveDataLogging(true));
            }

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
                        options.SaveTokens = true;
                    });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISlackService, SlackService>();
            services.AddScoped<HttpClient>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationContext applicationContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                AdminParser adminParser = new AdminParser(applicationContext, configuration);
            }

            if (env.IsProduction())
            {
                AdminParser adminParser = new AdminParser(applicationContext, configuration);
            }

            app.UseAuthentication();

            app.UseMvc();

        }
    }
}
