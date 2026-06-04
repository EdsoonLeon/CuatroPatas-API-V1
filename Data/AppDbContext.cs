using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Models;
using CuatroPatas.API.Models.SpResults;

namespace CuatroPatas.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<UsuarioRol> UsuarioRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Veterinario> Veterinarios { get; set; }
    public DbSet<HorarioVeterinario> HorariosVeterinario { get; set; }
    public DbSet<Mascota> Mascotas { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<Cita> Citas { get; set; }
    public DbSet<DetalleCita> DetallesCita { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<HistorialMedico> HistorialesMedicos { get; set; }
    public DbSet<Medicamento> Medicamentos { get; set; }
    public DbSet<Prescripcion> Prescripciones { get; set; }
    public DbSet<DocumentoMascota> DocumentosMascota { get; set; }
    public DbSet<Notificacion> Notificaciones { get; set; }
    public DbSet<Auditoria> Auditorias { get; set; }

    // Keyless entities for SP results
    public DbSet<AuthSpResult> AuthSpResults { get; set; }
    public DbSet<CitaSpResult> CitaSpResults { get; set; }
    public DbSet<CitaServicioSpResult> CitaServicioSpResults { get; set; }
    public DbSet<AgendaSpResult> AgendaSpResults { get; set; }
    public DbSet<EstadisticasSpResult> EstadisticasSpResults { get; set; }
    public DbSet<HistorialSpResult> HistorialSpResults { get; set; }
    public DbSet<PrescripcionSpResult> PrescripcionSpResults { get; set; }
    public DbSet<StockBajoSpResult> StockBajoSpResults { get; set; }
    public DbSet<DashboardSpResult> DashboardSpResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Composite PK for USUARIO_ROL
        modelBuilder.Entity<UsuarioRol>()
            .HasKey(ur => new { ur.IdUsuario, ur.IdRol });

        modelBuilder.Entity<UsuarioRol>()
            .HasOne(ur => ur.Usuario)
            .WithMany(u => u.UsuarioRoles)
            .HasForeignKey(ur => ur.IdUsuario);

        modelBuilder.Entity<UsuarioRol>()
            .HasOne(ur => ur.Rol)
            .WithMany(r => r.UsuarioRoles)
            .HasForeignKey(ur => ur.IdRol);

        // Mascota computed column
        modelBuilder.Entity<Mascota>()
            .Property(m => m.EdadCalculada)
            .ValueGeneratedOnAddOrUpdate();

        // Cita -> Mascota
        modelBuilder.Entity<Cita>()
            .HasOne(c => c.Mascota)
            .WithMany(m => m.Citas)
            .HasForeignKey(c => c.IdMascota)
            .OnDelete(DeleteBehavior.Restrict);

        // Cita -> Veterinario
        modelBuilder.Entity<Cita>()
            .HasOne(c => c.Veterinario)
            .WithMany(v => v.Citas)
            .HasForeignKey(c => c.IdVeterinario)
            .OnDelete(DeleteBehavior.Restrict);

        // Pago -> Cita (one-to-one)
        modelBuilder.Entity<Pago>()
            .HasOne(p => p.Cita)
            .WithOne(c => c.Pago)
            .HasForeignKey<Pago>(p => p.IdCita)
            .OnDelete(DeleteBehavior.Restrict);

        // HistorialMedico -> Mascota
        modelBuilder.Entity<HistorialMedico>()
            .HasOne(h => h.Mascota)
            .WithMany(m => m.Historiales)
            .HasForeignKey(h => h.IdMascota)
            .OnDelete(DeleteBehavior.Restrict);

        // HistorialMedico -> Veterinario
        modelBuilder.Entity<HistorialMedico>()
            .HasOne(h => h.Veterinario)
            .WithMany()
            .HasForeignKey(h => h.IdVeterinario)
            .OnDelete(DeleteBehavior.Restrict);

        // HistorialMedico -> Cita (optional)
        modelBuilder.Entity<HistorialMedico>()
            .HasOne(h => h.Cita)
            .WithMany(c => c.Historiales)
            .HasForeignKey(h => h.IdCita)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        // Keyless SP result entities
        modelBuilder.Entity<AuthSpResult>().HasNoKey();
        modelBuilder.Entity<CitaSpResult>().HasNoKey();
        modelBuilder.Entity<CitaServicioSpResult>().HasNoKey();
        modelBuilder.Entity<AgendaSpResult>().HasNoKey();
        modelBuilder.Entity<EstadisticasSpResult>().HasNoKey();
        modelBuilder.Entity<HistorialSpResult>().HasNoKey();
        modelBuilder.Entity<PrescripcionSpResult>().HasNoKey();
        modelBuilder.Entity<StockBajoSpResult>().HasNoKey();
        modelBuilder.Entity<DashboardSpResult>().HasNoKey();
    }
}
