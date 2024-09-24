using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using Serilog;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISummarizeService, SummarizeService>();
builder.Services.AddScoped<IYouTubeService, YouTubeService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "summify-api", Version = "v1.0.0" });
});

builder.Services.Configure<MySettings>(builder.Configuration.GetSection("Keys"));
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

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

