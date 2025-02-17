using iteraBus.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iteraBus.Repositorio.Configuration
{
    public class LocalizacaoConfiguration : IEntityTypeConfiguration<Localizacao>
    {
        public void Configure(EntityTypeBuilder<Localizacao> builder)
        {
            builder.ToTable("Localização").HasKey(l => l.Id);

            builder.Property(nameof(Localizacao.Id)).HasColumnName("LocalizacaoId").IsRequired(true);
            builder.Property(nameof(Localizacao.OnibusId)).HasColumnName("OnibusId").IsRequired(true);

            builder.Property(nameof(Localizacao.Latitude))
            .HasColumnName("Latitude")
            .IsRequired(true)
            .HasPrecision(10, 6);

            builder.Property(nameof(Localizacao.Latitude))
           .HasColumnName("Longitude")
           .IsRequired(true)
           .HasPrecision(10, 6);

            builder.Property(nameof(Localizacao.Horario))
            .HasColumnName("Horario")
            .IsRequired(true)
            .HasColumnType("datetime2");

            builder.HasOne(l => l.Onibus)
            .WithMany(o => o.Localizacoes)
            .HasForeignKey(l => l.OnibusId)
            .OnDelete(DeleteBehavior.Cascade);



        }
    }
}