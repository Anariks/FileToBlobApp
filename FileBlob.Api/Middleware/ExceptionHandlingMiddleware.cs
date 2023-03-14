using System.Net;
using System.Text.Json;
using Models;
using Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger
    )
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
        catch (AppException e)
        {
            await HandleExceptionAsync(context, e, e.ErrorCode);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext context,
        Exception e,
        ErrorCode code = ErrorCode.InternalServerError
    )
    {
        _logger.LogError("{type} , {e}", e.GetType(), e.Message);
        var message = e.Message;

        if (!(e is AppException))
            message = "Something went wrong, sorry..";

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        var error = new ErrorResponse { ErrorCode = code, Message = message };
        await context.Response.WriteAsync(JsonSerializer.Serialize(error));
    }
}
