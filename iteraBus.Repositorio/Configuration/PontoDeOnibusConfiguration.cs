using iteraBus.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iteraBus.Repositorio.Configuration
{
    public class PontoDeOnibusConfiguration : IEntityTypeConfiguration<PontoDeOnibus>
    {
        public void Configure(EntityTypeBuilder<PontoDeOnibus> builder)
        {
            builder.ToTable("PontosDeOnibus").HasKey(p => p.Id);

            builder.Property(nameof(PontoDeOnibus.Id)).HasColumnName("PontoDeOnibusId").IsRequired();
            builder.Property(nameof(PontoDeOnibus.Nome)).HasColumnName("Nome").IsRequired();
            builder.Property(nameof(PontoDeOnibus.Longitude)).HasColumnName("Longitude").IsRequired();
            builder.Property(nameof(PontoDeOnibus.Latitude)).HasColumnName("Latitude").IsRequired();

            builder.HasOne(p => p.Rota)
                .WithMany(r => r.PontosDeOnibus)
                .HasForeignKey(p => p.RotaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}