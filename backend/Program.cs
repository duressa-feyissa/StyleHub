using Application;
using Infrastructure.Configuration;

using Microsoft.OpenApi.Models;
using Persistence.Configuration;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistenceService(builder.Configuration, builder.Environment);
builder.Services.ConfigureInfrastructureService(builder.Configuration, builder.Environment);
builder.Services.ConfigureApplicationServices();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(
        "AllowAnyOrigin",
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        }
    );
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "JWT Authorization header using the Bearer scheme.",
            Type = SecuritySchemeType.Http,
        }
    );
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        }
    );
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StyleHub.WebApi v1");
});
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseCors("AllowAnyOrigin");
app.MapControllers();
app.Run();
