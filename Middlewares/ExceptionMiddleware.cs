// ═══════════════════════════════════════════════════════
// ARCHIVO: ExceptionMiddleware.cs
// QUÉ HACE: Es el "guardia de errores" de la aplicación.
//           Intercepta CUALQUIER excepción no controlada que ocurra en la API
//           y la convierte en una respuesta JSON limpia y uniforme.
//           Sin este middleware, los errores mostrarían el stack trace interno al cliente.
// QUIÉN LO USA: Program.cs — se registra como el PRIMER middleware del pipeline
// ═══════════════════════════════════════════════════════

using System.Net;
using System.Text.Json;
using CuatroPatas.API.Helpers;

namespace CuatroPatas.API.Middlewares;

// Un Middleware es como un guardia en la puerta de un edificio.
// Cada petición HTTP que llega pasa por aquí antes de llegar al Controller.
// Si algo sale mal en cualquier punto, este guardia lo captura y responde ordenadamente.
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Deja pasar la petición al siguiente paso del pipeline.
    /// Si en algún punto se lanza una excepción, la captura y la maneja.
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Intentamos procesar la petición normalmente
            await _next(context);
        }
        catch (Exception ex)
        {
            // Si algo falló, lo registramos en los logs para poder investigarlo después
            _logger.LogError(ex, "Excepción no controlada: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Convierte la excepción en una respuesta JSON con el código HTTP apropiado.
    /// Todas las respuestas de error tienen el mismo formato: { status, message, timestamp }
    /// </summary>
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Según el tipo de excepción, asignamos el código HTTP correspondiente.
        // Cada tipo de excepción fue diseñado para mapear a un código HTTP específico.
        var (statusCode, message) = exception switch
        {
            NotFoundException       => (HttpStatusCode.NotFound, exception.Message),       // 404
            UnauthorizedException   => (HttpStatusCode.Unauthorized, exception.Message),   // 401
            ForbiddenException      => (HttpStatusCode.Forbidden, exception.Message),      // 403
            ConflictException       => (HttpStatusCode.Conflict, exception.Message),       // 409
            BadRequestException     => (HttpStatusCode.BadRequest, exception.Message),     // 400
            // Para errores inesperados (bugs reales), devolvemos un mensaje genérico.
            // Nunca enviamos el stack trace ni detalles internos al cliente —
            // un atacante podría usar esa información para explotar vulnerabilidades.
            _ => (HttpStatusCode.InternalServerError, "Ocurrió un error interno del servidor.")
        };

        context.Response.StatusCode = (int)statusCode;

        // Construimos la respuesta JSON con formato uniforme
        var response = new
        {
            status = (int)statusCode,
            message,
            timestamp = DateTime.UtcNow // Para que el frontend o los logs sepan cuándo ocurrió
        };

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase // camelCase en el JSON de respuesta
        });

        await context.Response.WriteAsync(json);
    }
}
