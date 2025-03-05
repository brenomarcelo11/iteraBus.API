using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                optionsBuilder.UseSqlServer("Server=BRENO;Database=IteraBus;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IteraBusContexto>(options =>
                options.UseSqlServer(
                    "Server=BRENO;Database=IteraBus;Trusted_Connection=True;TrustServerCertificate=True;",
                    sqlOptions => sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,             // Número máximo de tentativas
                        maxRetryDelay: TimeSpan.FromSeconds(5), // Tempo máximo de espera entre tentativas
                        errorNumbersToAdd: null)      // Lista de códigos de erro específicos a serem tratados
                ));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OnibusConfiguration());
            modelBuilder.ApplyConfiguration(new LocalizacaoConfiguration());
            modelBuilder.ApplyConfiguration(new RotaConfiguration());
        }
    }
}