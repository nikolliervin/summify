using System.Net;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
public class ExceptionMiddleware {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;    
    }

    public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context){
        try{
            await _next(context); 
        }
        catch(Exception ex){
            _logger.LogError(ex, "An unhandled exception has occurred: {Message}", ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error. Please try again later."
            };

            var jsonResponse = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}