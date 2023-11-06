namespace Groups.Query.Api.Extensions;

public class ExceptionHandlingMiddleware:IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error has occured: {e.Message}");
            await ExceptionHandling(context, e);
        }
    }
    
    private static async Task ExceptionHandling(HttpContext context, Exception e)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(e.Message);
    }
}