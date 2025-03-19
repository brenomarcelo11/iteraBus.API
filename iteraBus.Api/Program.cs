using iteraBus.Aplicacao;
using iteraBus.Aplicacao.Interfaces;
using iteraBus.Dominio.Repositorios;
using iteraBus.Repositorio;
using iteraBus.Repositorio.Contexto;
using iteraBus.Repositorio.Inteface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados com a string de conexão
builder.Services.AddDbContext<IteraBusContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configuração de outros serviços (repositorios, controllers, etc)
builder.Services.AddScoped<IOnibusAplicacao, OnibusAplicacao>();
builder.Services.AddScoped<IOnibusRepositorio, OnibusRepositorio>();
builder.Services.AddScoped<IRotaAplicacao, RotaAplicacao>();
builder.Services.AddScoped<IRotaRepositorio, RotaRepositorio>();
builder.Services.AddScoped<ILocalizacaoAplicacao, LocalizacaoAplicacao>();
builder.Services.AddScoped<ILocalizacaoRepositorio, LocalizacaoRepositorio>();
builder.Services.AddScoped<IPontoDeOnibusAplicacao, PontoDeOnibusAplicacao>();
builder.Services.AddScoped<IPontoDeOnibusRepositorio, PontoDeOnibusRepositorio>();
// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Configuração do Swagger e Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração de ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("PermitirTudo");
app.UseAuthorization();
app.MapControllers();

app.Run();
