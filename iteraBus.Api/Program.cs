using iteraBus.Aplicacao;
using iteraBus.Aplicacao.Interfaces;
using iteraBus.Repositorio;
using iteraBus.Repositorio.Contexto;
using iteraBus.Repositorio.Inteface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<IteraBusContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionando serviços da aplicação
builder.Services.AddScoped<IOnibusAplicacao, OnibusAplicacao>();
builder.Services.AddScoped<IOnibusRepositorio, OnibusRepositorio>();

builder.Services.AddScoped<IRotaAplicacao, RotaAplicacao>();
builder.Services.AddScoped<IRotaRepositorio, RotaRepositorio>();

builder.Services.AddScoped<ILocalizacaoAplicacao, LocalizacaoAplicacao>();
builder.Services.AddScoped<ILocalizacaoRepositorio, LocalizacaoRepositorio>();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Adicionando serviços da API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do ambiente
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
