using Serilog;
using System.Diagnostics;
using System.Text;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var request = await FormatRequest(context.Request);


        var correlationId = context.TraceIdentifier;
        context.Response.Headers.Add("X-Correlation-Id", correlationId);

        Log.Information("Incoming Request {@CorrelationId} {@Method} {@Path} {@Headers} {@Body}",
            correlationId,
            context.Request.Method,
            context.Request.Path,
            context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
            request);

        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        stopwatch.Stop();

        var response = await FormatResponse(context.Response);

        Log.Information("Outgoing Response {@CorrelationId} {@StatusCode} {@DurationMs} {@Body}",
            correlationId,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds,
            response);

        await responseBody.CopyToAsync(originalBodyStream);
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        var query = request.QueryString.HasValue ? request.QueryString.Value : "";
        var route = request.Path.Value ?? "";

        string body = "";
        if (request.ContentLength > 0)
        {
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            request.Body.Read(buffer, 0, buffer.Length);
            body = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0;
        }

        return $"Route: {route}, Query: {query}, Body: {body}";
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return text;
    }

}
