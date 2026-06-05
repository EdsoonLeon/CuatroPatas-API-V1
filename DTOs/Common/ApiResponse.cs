// ═══════════════════════════════════════════════════════
// ARCHIVO: ApiResponse.cs
// QUÉ HACE: El "sobre estándar" que envuelve todas las respuestas de la API.
//           Garantiza que el frontend siempre reciba la misma estructura JSON,
//           ya sea éxito o error: { success, message, data }.
//           Evita que cada controller invente su propio formato de respuesta.
// QUIÉN LO USA: Todos los Controllers (envuelven sus respuestas con Ok() o Fail())
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    // Los datos de la respuesta — null en respuestas de error
    public T? Data { get; set; }

    /// <summary>
    /// Crea una respuesta exitosa.
    /// Ejemplo: return Ok(ApiResponse&lt;MascotaResponse&gt;.Ok(resultado));
    /// </summary>
    public static ApiResponse<T> Ok(T data, string? message = null) =>
        new() { Success = true, Data = data, Message = message };

    /// <summary>
    /// Crea una respuesta de error con un mensaje descriptivo para el usuario.
    /// El ExceptionMiddleware también usa este formato para errores no controlados.
    /// </summary>
    public static ApiResponse<T> Fail(string message) =>
        new() { Success = false, Message = message };
}
