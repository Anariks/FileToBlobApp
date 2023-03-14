using System.Net;
using System.Text.Json;
using Models;
using Exceptions;

public class RedirectToIndexMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public RedirectToIndexMiddleware(
        RequestDelegate next,
        ILogger<RedirectToIndexMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
        {
            context.Request.Path = "/index.html";
            await _next(context);
        }
    }
}
