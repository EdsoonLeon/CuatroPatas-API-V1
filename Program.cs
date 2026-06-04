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

// 1. DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CuatroPatasDB")));

// 2. JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)),
            ClockSkew = TimeSpan.Zero
        };
    });

// 3. Authorization con politicas
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SoloAdmin", policy =>
        policy.RequireRole("Administrador"));

    options.AddPolicy("PersonalClinica", policy =>
        policy.RequireRole("Administrador", "Veterinario", "Recepcionista"));

    options.AddPolicy("VetOAdmin", policy =>
        policy.RequireRole("Administrador", "Veterinario"));
});

// 4. CORS
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

// 5. AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// 6. Swagger con Bearer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CuatroPatas API",
        Version = "v1",
        Description = "API Backend para la Clínica Veterinaria CuatroPatas"
    });

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

// 7. Inyeccion de dependencias
// Helpers
builder.Services.AddSingleton<JwtHelper>();

// Repositories
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

// Services
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

// 8. Middleware global de errores
app.UseMiddleware<ExceptionMiddleware>();

// 9. CORS
app.UseCors("FrontendPolicy");

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

// 10. Authentication
app.UseAuthentication();

// 11. Authorization
app.UseAuthorization();

// 12. Controllers
app.MapControllers();

app.Run();
