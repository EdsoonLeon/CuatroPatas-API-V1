// ═══════════════════════════════════════════════════════
// ARCHIVO: AppExceptions.cs
// QUÉ HACE: Define los tipos de error personalizados que puede lanzar la aplicación.
//           Cada tipo de excepción corresponde a un código HTTP específico.
//           El ExceptionMiddleware las captura y las convierte en respuestas JSON.
// QUIÉN LO USA: Services y Repositories cuando ocurre un error de negocio
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.Helpers;

// Se lanza cuando se busca algo que no existe en la base de datos.
// Ejemplo: GET /api/cliente/999 cuando no hay cliente con ID 999.
// El ExceptionMiddleware lo convierte en: HTTP 404 Not Found
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

// Se lanza cuando las credenciales son incorrectas o el token es inválido.
// Ejemplo: contraseña incorrecta, token expirado, cuenta bloqueada.
// El ExceptionMiddleware lo convierte en: HTTP 401 Unauthorized
public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message) { }
}

// Se lanza cuando el usuario está autenticado pero no tiene permiso para esa acción.
// Ejemplo: un Cliente intentando acceder a un endpoint que es solo para Administradores.
// El ExceptionMiddleware lo convierte en: HTTP 403 Forbidden
public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message) { }
}

// Se lanza cuando se intenta crear algo que ya existe y viola una regla de unicidad.
// Ejemplo: registrarse con un email que ya tiene cuenta, o crear un servicio duplicado.
// El ExceptionMiddleware lo convierte en: HTTP 409 Conflict
public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}

// Se lanza cuando los datos recibidos en la petición son inválidos o inconsistentes.
// Ejemplo: fechas en formato incorrecto, valores fuera de rango permitido.
// El ExceptionMiddleware lo convierte en: HTTP 400 Bad Request
public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}
