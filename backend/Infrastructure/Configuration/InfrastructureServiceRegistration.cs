using System.Text;
using Application.Common;
using Application.Contracts.Infrastructure.Repositories;
using Application.Contracts.Infrastructure.Services;
using Infrastructure.Models;
using Infrastructure.Repository;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Configuration
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureService(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment hostEnvironment
        )
        {
            EmailSettings emailSettings = new EmailSettings();
            PhoneNumberOTPSettings phoneNumberOTPSettings = new PhoneNumberOTPSettings();
            ApiSettings apiSetting = new ApiSettings();
            JwtSettings jwtSettings = new JwtSettings();
            CloudinarySettings cloudinarySettings = new CloudinarySettings();

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
                emailSettings.SenderEmail = Environment.GetEnvironmentVariable("SenderEmail");
                emailSettings.SenderPassword = Environment.GetEnvironmentVariable("SenderPassword");
                emailSettings.DisplayName = Environment.GetEnvironmentVariable("DisplayName");
                services.AddSingleton(emailSettings);
                phoneNumberOTPSettings.AccountSid = Environment.GetEnvironmentVariable(
                    "AccountSid"
                );
                phoneNumberOTPSettings.AuthToken = Environment.GetEnvironmentVariable("AuthToken");
                phoneNumberOTPSettings.PhoneNumber = Environment.GetEnvironmentVariable(
                    "PhoneNumber"
                );
                services.AddSingleton(phoneNumberOTPSettings);

                apiSetting.SecretKey = Environment.GetEnvironmentVariable("SecretKey");
                services.AddSingleton(apiSetting);

                jwtSettings.Key = Environment.GetEnvironmentVariable("JwtKey");
                jwtSettings.Issuer = Environment.GetEnvironmentVariable("Issuer");
                jwtSettings.Audience = Environment.GetEnvironmentVariable("Audience");
                services.AddSingleton(jwtSettings);

                cloudinarySettings.CloudName = Environment.GetEnvironmentVariable("CloudName");
                cloudinarySettings.APIKey = Environment.GetEnvironmentVariable("APIKey");
                cloudinarySettings.APISecret = Environment.GetEnvironmentVariable("APISecret");
                services.AddSingleton(cloudinarySettings);

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
                            ValidIssuer = jwtSettings.Issuer,
                            ValidAudience = jwtSettings.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtSettings.Key ?? "")
                            )
                        };
                    });
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
