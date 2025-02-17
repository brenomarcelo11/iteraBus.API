using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Configuration;
using Microsoft.EntityFrameworkCore;

namespace iteraBus.Repositorio.Contexto
{
    public class IteraBusContexto : DbContext
    {
        private readonly DbContextOptions _options;

        public DbSet<Onibus> Onibus { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<Rota> Rotas { get; set; }

        public IteraBusContexto(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options == null)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-B4D87ME\\SQLEXPRESS01;Database=IteraBus;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OnibusConfiguration());
            modelBuilder.ApplyConfiguration(new LocalizacaoConfiguration());
            modelBuilder.ApplyConfiguration(new RotaConfiguration());
        }
    }
}