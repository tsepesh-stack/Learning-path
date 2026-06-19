using System;
using System.Diagnostics;
namespace TaskManagerApi;
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
        
        await _next(context);  // передаём запрос дальше
        sw.Stop();
        System.Console.WriteLine($"{context.Request.Method} {context.Request.Path} - {sw.ElapsedMilliseconds} мс");
    }
}