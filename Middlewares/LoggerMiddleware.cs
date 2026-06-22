namespace ExpenseTracker.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
         public async Task InvokeAsync(HttpContext context)
            {
            var method = context.Request.Method;
            var url = context.Request.Path;

            Console.WriteLine($"[LOG] Incoming Request: {method} {url}");

            await _next(context);

            var statusCode = context.Response.StatusCode;
            Console.WriteLine($"[LOG] Outgoing Response Status: {statusCode}");
        }
    }
}