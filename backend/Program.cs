using Application;
using Infrastructure;
using Persistence.Configuration;
using WebApi.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseRouting();

app.UseCors("AllowAnyOrigin");

app.UseEndpoints(endpoints =>
{
	_ = endpoints.MapControllers();
});

app.Run();
