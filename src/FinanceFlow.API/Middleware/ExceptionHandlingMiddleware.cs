using System.Net;
using System.Text.Json;

namespace FinanceFlow.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception exception)
        {
            _logger.LogError(exception, "Erro não tratado: {Message}", exception.Message);

            await HandleExceptionAsync(context, exception);
        }
    }
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        int statusCode;
        string message;

        switch (exception)
        {
            case ArgumentException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = exception.Message;
                break;

            case InvalidOperationException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = exception.Message;
                break;

            case KeyNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                message = exception.Message;
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = "Ocorreu um erro interno. Tente novamente mais tarde.";
                break;
        }

        context.Response.StatusCode = statusCode;

        var response = new
        {
            status = statusCode,
            message = message
        };

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}