using Microsoft.OpenApi.Models;
using Application;
using WebApi.Middlewares;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistenceService(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
 c =>
 {
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "StyleHub.WebApi", Version = "v1" });
 }
);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


