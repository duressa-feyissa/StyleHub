using System.Text;
using backend.Application.Common;
using backend.Application.Contracts.Infrastructure.Repositories;
using backend.Application.Contracts.Infrastructure.Services;
using backend.Infrastructure.Models;
using backend.Infrastructure.Repository;
using Microsoft.IdentityModel.Tokens;

namespace backend.Infrastructure.Configuration
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureService(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment hostEnvironment
        )
        {
            var cloudinarySettings = new CloudinarySettings();
            if (hostEnvironment.IsDevelopment())
            {
                services.Configure<EmailSettings>(options =>
                    configuration.GetSection("EmailSettings").Bind(options)
                );

                services.Configure<PhoneNumberOTPSettings>(options =>
                    configuration.GetSection("PhoneNumberOTPSettings").Bind(options)
                );

                services.Configure<ApiSettings>(options =>
                    configuration.GetSection("ApiSettings").Bind(options)
                );

                services.Configure<JwtSettings>(options =>
                    configuration.GetSection("JwtSettings").Bind(options)
                );

                services.Configure<CloudinarySettings>(options =>
                    configuration.GetSection("CloudinarySettings").Bind(options)
                );

                cloudinarySettings.CloudName = configuration["CloudinarySettings:CloudName"];
                cloudinarySettings.APIKey = configuration["CloudinarySettings:APIKey"];
                cloudinarySettings.APISecret = configuration["CloudinarySettings:APISecret"];

                services
                    .AddAuthentication(
                        Microsoft
                            .AspNetCore
                            .Authentication
                            .JwtBearer
                            .JwtBearerDefaults
                            .AuthenticationScheme
                    )
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = configuration["JwtSettings:Issuer"],
                            ValidAudience = configuration["JwtSettings:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"] ?? "")
                            )
                        };
                    });
            }
            else
            {
                services.Configure<EmailSettings>(options =>
                {
                    options.SenderEmail = Environment.GetEnvironmentVariable("SenderEmail");
                    options.SenderPassword = Environment.GetEnvironmentVariable("SenderPassword");
                    options.DisplayName = Environment.GetEnvironmentVariable("DisplayName");
                });

                services.Configure<PhoneNumberOTPSettings>(options =>
                {
                    options.AccountSid = Environment.GetEnvironmentVariable("AccountSid");
                    options.AuthToken = Environment.GetEnvironmentVariable("AuthToken");
                    options.PhoneNumber = Environment.GetEnvironmentVariable("PhoneNumber");
                });

                services.Configure<ApiSettings>(options =>
                {
                    options.SecretKey = Environment.GetEnvironmentVariable("SecretKey");
                });

                services.Configure<JwtSettings>(options =>
                {
                    options.Key = Environment.GetEnvironmentVariable("JwtKey");
                    options.Issuer = Environment.GetEnvironmentVariable("Issuer");
                    options.Audience = Environment.GetEnvironmentVariable("Audience");
                });

                services.Configure<CloudinarySettings>(options =>
                {
                    options.CloudName = Environment.GetEnvironmentVariable("CloudName");
                    options.APIKey = Environment.GetEnvironmentVariable("APIKey");
                    options.APISecret = Environment.GetEnvironmentVariable("APISecret");
                });

                var key = Encoding.ASCII.GetBytes(
                    Environment.GetEnvironmentVariable("JwtKey") ?? ""
                );

                services
                    .AddAuthentication(
                        Microsoft
                            .AspNetCore
                            .Authentication
                            .JwtBearer
                            .JwtBearerDefaults
                            .AuthenticationScheme
                    )
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Environment.GetEnvironmentVariable("Issuer"),
                            ValidAudience = Environment.GetEnvironmentVariable("Audience"),
                            IssuerSigningKey = new SymmetricSecurityKey(key)
                        };
                    });

                Console.WriteLine(
                    "EmailSettings: " + Environment.GetEnvironmentVariable("SenderEmail")
                );
                Console.WriteLine(
                    "PhoneNumberOTPSettings: " + Environment.GetEnvironmentVariable("AccountSid")
                );
                Console.WriteLine(
                    "ApiSettings: " + Environment.GetEnvironmentVariable("SecretKey")
                );
                Console.WriteLine("JwtSettings: " + Environment.GetEnvironmentVariable("JwtKey"));
                Console.WriteLine(
                    "CloudinarySettings: " + Environment.GetEnvironmentVariable("CloudName")
                );
                Console.WriteLine(
                    "CloudinarySettings: " + Environment.GetEnvironmentVariable("APIKey")
                );
                Console.WriteLine(
                    "CloudinarySettings: " + Environment.GetEnvironmentVariable("APISecret")
                );
                Console.WriteLine("JwtSettings: " + Environment.GetEnvironmentVariable("Issuer"));
                Console.WriteLine("JwtSettings: " + Environment.GetEnvironmentVariable("Audience"));
                Console.WriteLine("JwtSettings: " + Environment.GetEnvironmentVariable("JwtKey"));
                Console.WriteLine("SecretKey: " + Environment.GetEnvironmentVariable("SecretKey"));
                cloudinarySettings.CloudName = Environment.GetEnvironmentVariable("CloudName");
                cloudinarySettings.APIKey = Environment.GetEnvironmentVariable("APIKey");
                cloudinarySettings.APISecret = Environment.GetEnvironmentVariable("APISecret");
            }

            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton(CloudinaryConfiguration.Configure(cloudinarySettings));
            services.AddHttpClient<PhoneNumberOTPManager>();
            services.AddHttpContextAccessor();
            services.AddSingleton<PhoneNumberOTPManager>();
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ICurrentLoggedInService, CurrentLoggedInService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
                options.AddPolicy("User", policy => policy.RequireRole("user"));
            });

            return services;
        }
    }
}
