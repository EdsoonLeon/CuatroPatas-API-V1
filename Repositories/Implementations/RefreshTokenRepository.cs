using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;

namespace CuatroPatas.API.Repositories.Implementations;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context) => _context = context;

    public async Task<RefreshToken?> GetByTokenAsync(string token) =>
        await _context.RefreshTokens
            .Include(r => r.Usuario)
            .FirstOrDefaultAsync(r => r.Token == token);

    public async Task<RefreshToken> CreateAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
        return refreshToken;
    }

    public async Task RevokeAsync(string token)
    {
        var rt = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
        if (rt != null)
        {
            rt.Revocado = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteExpiredAsync()
    {
        var expired = _context.RefreshTokens
            .Where(r => r.FechaExpiracion < DateTime.UtcNow);
        _context.RefreshTokens.RemoveRange(expired);
        await _context.SaveChangesAsync();
    }
}
