using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
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
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OnlineQueuing.Data;
using OnlineQueuing.Helpers;
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

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Admin", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.Requirements.Add(new AdminRequirement());
                });
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:Jwt:Secret"])),
                            ClockSkew = TimeSpan.Zero
                        };
                        options.Events = new JwtBearerEvents()
                        {
                            OnAuthenticationFailed = c =>
                            {
                                c.NoResult();
                                c.Response.StatusCode = 401;
                                c.Response.ContentType = "application/json";
                                c.Response.WriteAsync(JsonConvert.SerializeObject(new CustomErrorMessage("Unauthorized"))).Wait();
                                return Task.CompletedTask;
                            },
                        };
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
            services.AddScoped<ISlackService, SlackService>();
            services.AddScoped<HttpClient>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationContext applicationContext)
        {
            AdminParser adminParser = new AdminParser(applicationContext, configuration);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                adminParser.FillUpDbWithAdmins();
            }

            if (env.IsProduction())
            {
                adminParser.FillUpDbWithAdmins();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
