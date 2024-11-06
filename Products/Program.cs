using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Products.Data;
using Products.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    // Add a default liveness check to ensure app is responsive
    .AddCheck("myself", () => HealthCheckResult.Unhealthy());

builder.AddServiceDefaults();

builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddSingleton<RandomFailureMiddleware>();


builder.Services.AddDbContext<ProductDataContext>(options =>
	options.UseInMemoryDatabase("inmemproducts"));

// Add services to the container.
var app = builder.Build();

app.MapDefaultEndpoints();

app.UseOutputCache();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseMiddleware<RandomFailureMiddleware>();

app.MapProductEndpoints();

app.UseStaticFiles();

app.CreateDbIfNotExists();

app.Run();


public class RandomFailureMiddleware : IMiddleware
{
	public Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		var path = context.Request.Path.Value;

		if (path is null || !path.Contains("api/Product", StringComparison.InvariantCultureIgnoreCase))
			return next(context);

		if (Random.Shared.NextDouble() >= 0.6)
		{
			throw new Exception("Computer says no.");
		}
		return next(context);
	}
}
