using BookStoreWebGentle.Data;
using BookStoreWebGentle.Helper;
using BookStoreWebGentle.JWTToken;
using BookStoreWebGentle.Models;
using BookStoreWebGentle.Repository;
using BookStoreWebGentle.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWebGentle
{

    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtSettings = new JwtSettings();
            _configuration.Bind("JwtSettings", jwtSettings);
            services.AddSingleton(jwtSettings);
            services.AddTransient<JwtTokenCreator>();
            services.AddDbContext<BookStoreContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(i =>
            {
                i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                i.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                i.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                       // ClockSkew = jwtSettings.Expire
                    };
                    options.SaveToken = true;
                    options.Events = new JwtBearerEvents();
                    options.Events.OnMessageReceived = context => {

                        if (context.Request.Cookies.ContainsKey("Access-Token"))
                        {
                            context.Token = context.Request.Cookies["Access-Token"];
                        }

                        return Task.CompletedTask;
                    };
                })
                .AddCookie(options =>
                {
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.IsEssential = true;
                });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
            });


            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(20);
            });

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Account/Login";
                option.ExpireTimeSpan = TimeSpan.FromHours(1);
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });

            services.AddControllersWithViews();
#if DEBUG
            services.AddRazorPages();
            //  option =>
            //{
            //  option.HtmlHelperOptions.ClientValidationEnabled = false;
            //
            //  });
#endif
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IContactRepository, ContactRepository>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.Configure<SMTPConfigModel>(_configuration.GetSection("SMTPConfig"));
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseStaticFiles();
                app.UseSession();
                app.Use(async (context, next) =>
                {
                    var token = context.Session.GetString("Token");
                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Request.Headers.Add("Authorization", "Bearer " + token);
                    }
                    await next();
                });

                app.UseAuthentication();
                app.UseRouting();
                app.UseAuthorization();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                    //endpoints.MapControllerRoute(
                    //    name: "Default",
                    //    pattern: "bookApp/{controller=Home}/{action=Index}/{id?}");

                    endpoints.MapControllerRoute(
                            name: "MyArea",
                            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                });
            }
        }
    }
}
