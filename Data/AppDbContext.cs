// ═══════════════════════════════════════════════════════
// ARCHIVO: AppDbContext.cs
// QUÉ HACE: Es el "puente" entre el código C# y la base de datos SQL Server.
//           Contiene una propiedad (DbSet) por cada tabla que usamos.
//           También configura las relaciones, restricciones y casos especiales
//           que no se pueden expresar solo con atributos en los modelos.
// QUIÉN LO USA: Todos los Repositories para consultar y guardar datos
// ═══════════════════════════════════════════════════════

using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Models;
using CuatroPatas.API.Models.SpResults;

namespace CuatroPatas.API.Data;

// DbContext es la clase base de Entity Framework Core.
// Hereda de ella y configuramos nuestras 18 tablas más los 9 tipos para Stored Procedures.
public class AppDbContext : DbContext
{
    // El constructor recibe la configuración (incluida la cadena de conexión)
    // que fue registrada en Program.cs → AddDbContext<AppDbContext>(...)
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // ─── TABLAS DE LA BASE DE DATOS ───────────────────────────────────────────
    // Cada DbSet<T> representa una tabla real en SQL Server.
    // El nombre de la propiedad no importa; el nombre de la tabla viene del atributo [Table] en el modelo.
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

    // ─── TIPOS PARA LEER RESULTADOS DE STORED PROCEDURES ─────────────────────
    // Estos NO son tablas reales — son clases que usamos para recibir los datos
    // que devuelven los Stored Procedures vía FromSqlRaw().
    // HasNoKey() en OnModelCreating le dice a EF que no intente rastrearlos ni hacer CRUD.
    public DbSet<AuthSpResult> AuthSpResults { get; set; }
    public DbSet<CitaSpResult> CitaSpResults { get; set; }
    public DbSet<CitaServicioSpResult> CitaServicioSpResults { get; set; }
    public DbSet<AgendaSpResult> AgendaSpResults { get; set; }
    public DbSet<EstadisticasSpResult> EstadisticasSpResults { get; set; }
    public DbSet<HistorialSpResult> HistorialSpResults { get; set; }
    public DbSet<HistorialCreateSpResult> HistorialCreateSpResults { get; set; }
    public DbSet<CitaCreateSpResult> CitaCreateSpResults { get; set; }
    public DbSet<CitaListSpResult> CitaListSpResults { get; set; }
    public DbSet<PrescripcionCreateSpResult> PrescripcionCreateSpResults { get; set; }
    public DbSet<PrescripcionSpResult> PrescripcionSpResults { get; set; }
    public DbSet<StockBajoSpResult> StockBajoSpResults { get; set; }
    public DbSet<DashboardSpResult> DashboardSpResults { get; set; }

    /// <summary>
    /// Configuraciones especiales del modelo que no se pueden poner
    /// como simples atributos en las entidades. Aquí van las relaciones complejas,
    /// claves compuestas y columnas computadas.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ─── CLAVE COMPUESTA EN USUARIO_ROL ───────────────────────────────────
        // La tabla USUARIO_ROL no tiene un ID propio — su clave primaria son
        // dos columnas juntas: (IdUsuario + IdRol). Esto garantiza que un usuario
        // no pueda tener el mismo rol asignado dos veces.
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

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("REFRESH_TOKEN");
            entity.HasKey(e => e.IdToken);
            entity.Property(e => e.IdToken).HasColumnName("id_token");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.FechaExpiracion).HasColumnName("fecha_expiracion");
            entity.Property(e => e.Revocado).HasColumnName("revocado");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
           
        });

        // ─── COLUMNA COMPUTADA EN MASCOTA ─────────────────────────────────────
        // edad_calculada es una columna que SQL Server calcula automáticamente
        // a partir de fecha_nacimiento. NUNCA la escribimos desde la app —
        // solo la leemos. ValueGeneratedOnAddOrUpdate le dice a EF que la refresque
        // después de cada INSERT o UPDATE.
        modelBuilder.Entity<Mascota>()
            .Property(m => m.EdadCalculada)
            .ValueGeneratedOnAddOrUpdate();

        // ─── RESTRICCIONES DE ELIMINACIÓN ─────────────────────────────────────
        // Usamos Restrict en lugar de Cascade para evitar borrados en cadena accidentales.
        // En este sistema NUNCA se borran registros físicamente — todo es baja lógica (Activo = false).
        // Si alguien intentara borrar una Mascota que tiene Citas, la BD lo rechazaría.

        modelBuilder.Entity<Cita>()
            .HasOne(c => c.Mascota)
            .WithMany(m => m.Citas)
            .HasForeignKey(c => c.IdMascota)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Cita>()
            .HasOne(c => c.Veterinario)
            .WithMany(v => v.Citas)
            .HasForeignKey(c => c.IdVeterinario)
            .OnDelete(DeleteBehavior.Restrict);

        // ─── PAGO: RELACIÓN UNO A UNO CON CITA ───────────────────────────────
        // Cada Cita tiene exactamente un Pago asociado (creado automáticamente por el SP).
        // HasOne/WithOne le dice a EF que es una relación 1:1, no 1:N.
        // Esto habilita la propiedad navegación cita.Pago directamente.
        modelBuilder.Entity<Pago>()
            .HasOne(p => p.Cita)
            .WithOne(c => c.Pago)
            .HasForeignKey<Pago>(p => p.IdCita)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HistorialMedico>()
            .HasOne(h => h.Mascota)
            .WithMany(m => m.Historiales)
            .HasForeignKey(h => h.IdMascota)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HistorialMedico>()
            .HasOne(h => h.Veterinario)
            .WithMany()
            .HasForeignKey(h => h.IdVeterinario)
            .OnDelete(DeleteBehavior.Restrict);

        // ─── HISTORIAL: CITA OPCIONAL ─────────────────────────────────────────
        // Un historial puede crearse sin tener una cita formal asociada
        // (ejemplo: vacunación directa, urgencia). Por eso IdCita es nullable
        // y si la Cita se elimina, el historial queda con IdCita = null (SetNull).
        modelBuilder.Entity<HistorialMedico>()
            .HasOne(h => h.Cita)
            .WithMany(c => c.Historiales)
            .HasForeignKey(h => h.IdCita)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        // ─── FK EXPLÍCITAS (patrón Id{Tipo} no es reconocido por la convención de EF) ──
        // EF Core espera {NavPropName}Id o {PrincipalType}Id como FK.
        // Nuestros modelos usan Id{TipoRelacionado} (ej: IdCliente), así que EF
        // generaría columnas shadow inexistentes (ej: ClienteIdCliente) sin estas config.

        modelBuilder.Entity<Mascota>()
            .HasOne(m => m.Cliente)
            .WithMany(c => c.Mascotas)
            .HasForeignKey(m => m.IdCliente)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Usuario)
            .WithMany()
            .HasForeignKey(c => c.IdUsuario)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Veterinario>()
            .HasOne(v => v.Usuario)
            .WithMany()
            .HasForeignKey(v => v.IdUsuario)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<HorarioVeterinario>()
            .HasOne(h => h.Veterinario)
            .WithMany(v => v.Horarios)
            .HasForeignKey(h => h.IdVeterinario)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DetalleCita>()
            .HasOne(d => d.Cita)
            .WithMany(c => c.DetallesCita)
            .HasForeignKey(d => d.IdCita)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DetalleCita>()
            .HasOne(d => d.Servicio)
            .WithMany(s => s.DetallesCita)
            .HasForeignKey(d => d.IdServicio)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DocumentoMascota>()
            .HasOne(d => d.Mascota)
            .WithMany(m => m.Documentos)
            .HasForeignKey(d => d.IdMascota)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Prescripcion>()
            .HasOne(p => p.HistorialMedico)
            .WithMany(h => h.Prescripciones)
            .HasForeignKey(p => p.IdHistorial)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Prescripcion>()
            .HasOne(p => p.Medicamento)
            .WithMany(m => m.Prescripciones)
            .HasForeignKey(p => p.IdMedicamento)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notificacion>()
            .HasOne(n => n.Usuario)
            .WithMany(u => u.Notificaciones)
            .HasForeignKey(n => n.IdUsuario)
            .OnDelete(DeleteBehavior.Restrict);

        // ─── TIPOS SIN CLAVE (para Stored Procedures) ────────────────────────
        // HasNoKey() le dice a EF que estos tipos no corresponden a tablas reales.
        // Solo sirven para recibir los resultados de FromSqlRaw("EXEC sp_Nombre...").
        // EF no intentará hacer INSERT, UPDATE ni DELETE con ellos.
        modelBuilder.Entity<AuthSpResult>().HasNoKey();
        modelBuilder.Entity<CitaSpResult>().HasNoKey();
        modelBuilder.Entity<CitaServicioSpResult>().HasNoKey();
        modelBuilder.Entity<AgendaSpResult>().HasNoKey();
        modelBuilder.Entity<EstadisticasSpResult>().HasNoKey();
        modelBuilder.Entity<HistorialSpResult>().HasNoKey();
        modelBuilder.Entity<HistorialCreateSpResult>().HasNoKey();
        modelBuilder.Entity<CitaCreateSpResult>().HasNoKey();
        modelBuilder.Entity<CitaListSpResult>().HasNoKey();
        modelBuilder.Entity<PrescripcionCreateSpResult>().HasNoKey();
        modelBuilder.Entity<PrescripcionSpResult>().HasNoKey();
        modelBuilder.Entity<StockBajoSpResult>().HasNoKey();
        modelBuilder.Entity<DashboardSpResult>().HasNoKey();

        // ─── TABLAS CON TRIGGERS ──────────────────────────────────────────────
        // SQL Server no permite OUTPUT sin INTO en tablas que tienen triggers activos.
        // EF Core 7+ usa OUTPUT INSERTED para recuperar IDs generados, lo que falla.
        // UseSqlOutputClause(false) hace que EF use SELECT SCOPE_IDENTITY() en su lugar.
        // Se aplica a todas las entidades reales (no keyless) para cubrir cualquier trigger.
        foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsKeyless))
        {
            modelBuilder.Entity(entityType.ClrType)
                .ToTable(t => t.UseSqlOutputClause(false));
        }
    }
}
