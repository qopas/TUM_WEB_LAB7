using System.Text.Json;
using Application.Exceptions;
using BookRental.Domain.Exceptions;
using BookRental.DTOs.Out;
using BookRental.Infrastructure.Exceptions;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace BookRental.Middleware;

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

            _ => (500, "An internal server error occurred", ["Please contact support"])
        };

        response.StatusCode = statusCode;

        var baseResponse = new BaseResponse<object>
        {
            Success = false,
            Message = message,
            Data = null,
            Errors = errors
        };

        var jsonResponse = JsonSerializer.Serialize(baseResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await response.WriteAsync(jsonResponse);
    }
}