using iteraBus.Aplicacao;
using iteraBus.Aplicacao.Interfaces;
using iteraBus.Dominio.Repositorios;
using iteraBus.Repositorio;
using iteraBus.Repositorio.Contexto;
using iteraBus.Repositorio.Inteface;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Projeto360.Aplicacao;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados com a string de conexão
builder.Services.AddDbContext<IteraBusContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Adiciona a autenticação JWT
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var jwtKey = builder.Configuration["Jwt:Key"];

if (string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience) || string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("Configuração JWT ausente no appsettings.json ou variáveis de ambiente.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
        policy.WithOrigins("http://localhost:3000") // Permite apenas a origem do React
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()); // Permite envio de cookies/tokens via credenciais
});

// Configuração dos serviços
builder.Services.AddScoped<IOnibusAplicacao, OnibusAplicacao>();
builder.Services.AddScoped<IOnibusRepositorio, OnibusRepositorio>();
builder.Services.AddScoped<IRotaAplicacao, RotaAplicacao>();
builder.Services.AddScoped<IRotaRepositorio, RotaRepositorio>();
builder.Services.AddScoped<ILocalizacaoAplicacao, LocalizacaoAplicacao>();
builder.Services.AddScoped<ILocalizacaoRepositorio, LocalizacaoRepositorio>();
builder.Services.AddScoped<IPontoDeOnibusAplicacao, PontoDeOnibusAplicacao>();
builder.Services.AddScoped<IPontoDeOnibusRepositorio, PontoDeOnibusRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();

// Configuração do Swagger com autenticação JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IteraBus API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
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
            new string[] { }
        }
    });
});

// Configuração dos controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configuração de ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("PermitirFrontend");  // CORS precisa vir antes de autenticação e autorização
app.UseAuthentication();  // Ativa autenticação JWT
app.UseAuthorization();   // Ativa autorização

app.MapControllers();

app.Run();
