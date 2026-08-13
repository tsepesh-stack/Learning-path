using System;
using System.Diagnostics;
namespace TaskManagerWebApi;
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        
        await _next(context);
        
        sw.Stop();
        Console.WriteLine($"{context.Request.Method} {context.Request.Path} - {sw.ElapsedMilliseconds}ms");
    }
}