// ═══════════════════════════════════════════════════════
// ARCHIVO: IRefreshTokenRepository.cs
// QUÉ HACE: Contrato del "cajero" que maneja los tokens de renovación en la BD.
//           Los RefreshTokens son los "códigos de renovación" de 7 días que permiten
//           obtener nuevos AccessTokens sin volver a pedir la contraseña.
//           DeleteExpiredAsync debería llamarse periódicamente (tarea programada)
//           para limpiar los tokens expirados y no inflar la tabla.
// QUIÉN LO USA: RefreshTokenRepository (implementación), AuthService
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<RefreshToken> CreateAsync(RefreshToken refreshToken);
    // Marca el token como Revocado = true — lo invalida sin borrarlo (para auditoría)
    Task RevokeAsync(string token);
    // Elimina físicamente los tokens que ya pasaron su fecha de expiración
    Task DeleteExpiredAsync();
}
