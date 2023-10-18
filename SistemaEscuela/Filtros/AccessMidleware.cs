using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class AccessMiddleware
{
    private readonly RequestDelegate _next;

    public AccessMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var usuario = context.Session.GetString("Usuario");

        if (usuario == null)
        {
            context.Response.Redirect("/Login/Index");
        }
        else
        {
            // Continuar con la solicitud si el usuario está autenticado
            await _next(context);
        }
    }
}
