using Application;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Persistence.Configuration;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Hello, World!");

builder.Services.ConfigurePersistenceService(builder.Configuration);
builder.Services.ConfigureInfrastructureService(builder.Configuration);
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StyleHub.WebApi", Version = "v1" });
});

var app = builder.Build();

// Configure Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StyleHub.WebApi v1");
});

// Middleware
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseCors("AllowAnyOrigin");

// Map controllers
app.MapControllers();

app.Run();
