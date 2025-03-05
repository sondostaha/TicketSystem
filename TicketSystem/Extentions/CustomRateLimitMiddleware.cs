namespace TicketSystem.Extentions
{
    public class CustomRateLimitMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomRateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check if the status code is 429 (Rate Limited)
            if (context.Response.StatusCode == 429)
            {
                context.Response.ContentType = "application/json";
                var response = new
                {
                    message = "Rate limit exceeded. Please try again later.",
                    retryAfter = context.Response.Headers["Retry-After"]
                };

                // Return the custom response
                await context.Response.WriteAsJsonAsync(response);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
