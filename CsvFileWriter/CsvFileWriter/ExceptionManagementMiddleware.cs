using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class ExceptionManagementMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionManagementMiddleware> _logger;

    public ExceptionManagementMiddleware(RequestDelegate next, ILogger<ExceptionManagementMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the middleware pipeline");

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorMessage = "An unexpected error occurred.";
            if (ex is CustomException customException)
            {
                response.StatusCode = (int)customException.StatusCode;
                errorMessage = customException.Message;
            }

            var errorResponse = JsonConvert.SerializeObject(new { error = errorMessage });
            await response.WriteAsync(errorResponse);
        }
    }
}

public class CustomException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public CustomException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        StatusCode = statusCode;
    }
}