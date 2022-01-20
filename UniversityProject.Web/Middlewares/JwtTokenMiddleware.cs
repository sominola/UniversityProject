namespace UniversityProject.Web.Middlewares;

public class JwtTokenMiddleware
{
    private readonly RequestDelegate _next;

    public JwtTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var contains = context.Request.Cookies.TryGetValue("Token", out var token);
        if (contains && !string.IsNullOrEmpty(token))
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
                context.Request.Headers.Add("Authorization", "Bearer " + token);
        }
        await _next.Invoke(context);
    }
}