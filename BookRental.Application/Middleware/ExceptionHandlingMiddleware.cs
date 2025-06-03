using System.Text.Json;
using Application.Exceptions;
using BookRental.Domain.Exceptions;
using BookRental.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An exception occurred: {Message}", exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var (statusCode, message, errors) = exception switch
        {
            EntityNotFoundException ex => (404, "Resource not found", ex.Errors),
            DomainException ex => (400, "Domain error", ex.Errors),

            ValidationException ex => (400, "Validation failed", ex.Errors),
            BusinessLogicException ex => (400, "Business logic error", ex.Errors),
            UnauthorizedException ex => (401, "Unauthorized", ex.Errors),
            ApplicationException ex => (400, "Application error", ex.Errors),

            DatabaseException ex => (500, "Database error", ex.Errors),
            InfrastructureException ex => (500, "Infrastructure error", ex.Errors),

            ArgumentNullException ex => (400, "Invalid argument", [ex.Message]),
            ArgumentException ex => (400, "Invalid argument", [ex.Message]),
            UnauthorizedAccessException => (401, "Unauthorized access", ["Access denied"]),
            TimeoutException ex => (408, "Request timeout", [ex.Message]),

            _ => (500, "An internal server error occurred", [exception.Message])
        };

        response.StatusCode = statusCode;

        var errorResponse = new ErrorResponse
        {
            Success = false,
            Message = message,
            Errors = errors
        };
        var environment = context.RequestServices.GetService<IWebHostEnvironment>();
        if (environment?.IsDevelopment() == true)
        {
            errorResponse.StackTrace = exception.StackTrace;
        }
        var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await response.WriteAsync(jsonResponse);
    }
}