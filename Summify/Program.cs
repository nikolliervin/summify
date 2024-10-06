using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using Serilog;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IYouTubeService, YoutubeService>();
builder.Services.AddScoped<IPdfService, PdfSummarizeService>();


// Register the common factory
builder.Services.AddScoped<ISummarizerFactory, SummarizerFactory>();
builder.Services.AddScoped<ITypeService, TypeService>();

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(), 
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                QueueLimit = 0,
                Window = TimeSpan.FromMinutes(1)
            }));
    
    options.OnRejected = (context, cancellationToken) =>
    {
        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            context.HttpContext.Response.Headers.RetryAfter = retryAfter.TotalSeconds.ToString();
        }

        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.");

        return new ValueTask();
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "summify-api", Version = "v1.0.0" });
});

builder.Services.Configure<MySettings>(builder.Configuration.GetSection("Keys"));
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseRateLimiter();
Log.Logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration)
.CreateLogger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

