// ═══════════════════════════════════════════════════════
// ARCHIVO: Program.cs
// QUÉ HACE: Punto de arranque de toda la aplicación.
//           Aquí se configuran todos los servicios que va a usar la API
//           y se define el orden en que se procesan las peticiones HTTP.
// QUIÉN LO USA: El servidor/sistema operativo al ejecutar la aplicación
// ═══════════════════════════════════════════════════════

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CuatroPatas.API.Data;
using CuatroPatas.API.Helpers;
using CuatroPatas.API.Middlewares;
using CuatroPatas.API.Repositories.Implementations;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Services.Implementations;
using CuatroPatas.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ─── 1. BASE DE DATOS ─────────────────────────────────────────────────────────
// Le decimos a la aplicación cómo conectarse a SQL Server.
// AppDbContext es el "puente" entre nuestro código C# y las tablas de la base de datos.
// La cadena de conexión viene del appsettings.json → "ConnectionStrings:CuatroPatasDB"
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CuatroPatasDB")));

// ─── 2. AUTENTICACIÓN JWT ─────────────────────────────────────────────────────
// Configuramos cómo validar el "carnet digital" (token JWT) que presenta el usuario
// en cada petición. Cada vez que llega un request con un token, este código lo verifica.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Verificar que el token fue emitido por esta API (no por otra aplicación)
            ValidateIssuer = true,
            // Verificar que el token está destinado para nuestro frontend específico
            ValidateAudience = true,
            // Verificar que el token no ha expirado (los tokens viven 60 minutos)
            ValidateLifetime = true,
            // Verificar que el token fue firmado con nuestra clave secreta
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)),
            // Sin margen de tolerancia en la expiración — si dice que expiró a las 3:00 PM, expiró a las 3:00 PM exacto
            ClockSkew = TimeSpan.Zero
        };
    });

// ─── 3. POLÍTICAS DE AUTORIZACIÓN ────────────────────────────────────────────
// Definimos "grupos de permiso" con nombres para usarlos en los Controllers.
// Es más limpio poner [Authorize(Policy="PersonalClinica")] que repetir todos los roles.
builder.Services.AddAuthorization(options =>
{
    // Solo el Administrador puede gestionar usuarios, veterinarios y medicamentos
    options.AddPolicy("SoloAdmin", policy =>
        policy.RequireRole("Administrador"));

    // Administrador, Veterinario y Recepcionista pueden acceder a citas, clientes, pagos, etc.
    options.AddPolicy("PersonalClinica", policy =>
        policy.RequireRole("Administrador", "Veterinario", "Recepcionista"));

    // Solo el veterinario o el admin pueden crear historiales y prescripciones médicas
    options.AddPolicy("VetOAdmin", policy =>
        policy.RequireRole("Administrador", "Veterinario"));
});

// ─── 4. CORS ──────────────────────────────────────────────────────────────────
// CORS (Cross-Origin Resource Sharing) permite que el frontend hable con esta API.
// Sin esta configuración, el navegador bloquearía todas las peticiones del frontend.
// localhost:5173 es el puerto que usa Vite al correr React en modo desarrollo.
// ⚠️  En producción, cambiar esta URL por la dirección real del sitio web desplegado.
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// ─── 5. AUTOMAPPER ────────────────────────────────────────────────────────────
// AutoMapper es un traductor automático entre entidades de la BD y los DTOs de respuesta.
// Gracias a él no tenemos que escribir "dto.Nombre = entidad.Nombre" para cada campo manualmente.
// Busca automáticamente la clase MappingProfile donde están definidas las conversiones.
builder.Services.AddAutoMapper(typeof(Program));

// ─── 6. SWAGGER ───────────────────────────────────────────────────────────────
// Swagger genera una página web interactiva donde se pueden ver y probar todos los endpoints.
// Solo se activa en modo Development (ver el bloque if más abajo).
// Para acceder: http://localhost:{puerto}/swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CuatroPatas API",
        Version = "v1",
        Description = "API Backend para la Clínica Veterinaria CuatroPatas"
    });

    // Habilitamos el botón "Authorize" en Swagger para pegar el token JWT
    // y poder probar los endpoints protegidos directamente desde el navegador
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization. Ingrese: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ─── 7. REGISTRO DE DEPENDENCIAS (Inyección de Dependencias) ─────────────────
// Aquí le decimos a ASP.NET qué clase concreta usar cuando alguien pida una interfaz.
// Ejemplo: cuando AuthService necesita un IClienteRepository, recibe un ClienteRepository.
// Esto desacopla el código: los Services no saben qué Repository usan exactamente.
//
//   AddSingleton  = una sola instancia para toda la vida de la app (para clases sin estado)
//   AddScoped     = instancia nueva por cada petición HTTP (se destruye al terminar el request)

// JwtHelper es Singleton porque solo lee configuración y no guarda datos entre peticiones
builder.Services.AddSingleton<JwtHelper>();

// ─ Repositories: cada uno sabe cómo hablar con una tabla específica de la base de datos ─
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IVeterinarioRepository, VeterinarioRepository>();
builder.Services.AddScoped<IMascotaRepository, MascotaRepository>();
builder.Services.AddScoped<IServicioRepository, ServicioRepository>();
builder.Services.AddScoped<IMedicamentoRepository, MedicamentoRepository>();
builder.Services.AddScoped<IHistorialMedicoRepository, HistorialMedicoRepository>();
builder.Services.AddScoped<IPrescripcionRepository, PrescripcionRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<INotificacionRepository, NotificacionRepository>();

// ─ Services: cada uno aplica las reglas de negocio de su área ─
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVeterinarioService, VeterinarioService>();
builder.Services.AddScoped<IMascotaService, MascotaService>();
builder.Services.AddScoped<ICitaService, CitaService>();
builder.Services.AddScoped<IHistorialMedicoService, HistorialMedicoService>();
builder.Services.AddScoped<IMedicamentoService, MedicamentoService>();
builder.Services.AddScoped<IPrescripcionService, PrescripcionService>();
builder.Services.AddScoped<IServicioService, ServicioService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddControllers();

var app = builder.Build();

// ═══════════════════════════════════════════════════════
// PIPELINE DE PETICIONES HTTP
// Cada petición que llega pasa por estos pasos EN ESTE ORDEN.
// El orden importa mucho — cambiar el orden puede romper la seguridad.
// ═══════════════════════════════════════════════════════

// El guardia de errores va PRIMERO para poder capturar cualquier excepción
// que ocurra en cualquier parte del proceso y devolver un JSON de error limpio
app.UseMiddleware<ExceptionMiddleware>();

// CORS va antes que los controllers para que el navegador no rechace la petición
// incluso antes de que llegue a ejecutar código de negocio
app.UseCors("FrontendPolicy");

// Swagger solo disponible en desarrollo local — NUNCA exponer en producción
// ya que mostraría toda la documentación interna de la API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CuatroPatas API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

// ¡ORDEN CRÍTICO! Authentication ANTES que Authorization.
// Primero identificamos quién es el usuario (leyendo y validando su token JWT).
// Después verificamos si esa persona tiene permiso para lo que quiere hacer.
// Si lo invertimos, intentaríamos verificar permisos sin saber aún quién es el usuario.
app.UseAuthentication();
app.UseAuthorization();

// Los Controllers reciben las peticiones ya autenticadas y autorizadas
app.MapControllers();

app.Run();
