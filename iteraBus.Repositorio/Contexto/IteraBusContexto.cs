using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Configuration;
using Microsoft.EntityFrameworkCore;

namespace iteraBus.Repositorio.Contexto
{
    public class IteraBusContexto : DbContext
    {
        public DbSet<Onibus> Onibus { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<Rota> Rotas { get; set; }
        public DbSet<PontoDeOnibus> PontosDeOnibus { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public IteraBusContexto()
        {
        }

        public IteraBusContexto(DbContextOptions<IteraBusContexto> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=BRENO\\SQLEXPRESS;Database=IteraBus;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OnibusConfiguration());
            modelBuilder.ApplyConfiguration(new LocalizacaoConfiguration());
            modelBuilder.ApplyConfiguration(new RotaConfiguration());
            modelBuilder.ApplyConfiguration(new PontoDeOnibusConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        }
    }
}
