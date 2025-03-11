using iteraBus.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iteraBus.Repositorio.Configuration
{
    public class OnibusConfiguration : IEntityTypeConfiguration<Onibus>
    {
        public void Configure(EntityTypeBuilder<Onibus> builder)
        {
            builder.ToTable("Onibus").HasKey(o => o.Id);

            builder.Property(nameof(Onibus.Id)).HasColumnName("OnibusId").IsRequired(true);

            builder.Property(nameof(Onibus.Placa))
            .HasColumnName("Placa")
            .IsRequired(true)
            .HasMaxLength(10);

            builder.Property(nameof(Onibus.RotaId)).HasColumnName("Rota").IsRequired(true);

            builder.HasOne(o => o.Rota)
            .WithMany(r => r.Onibus)
            .HasForeignKey(o => o.RotaId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Localizacoes)
            .WithOne(l => l.Onibus)
            .HasForeignKey(l => l.OnibusId)
            .OnDelete(DeleteBehavior.Cascade);


        }
    }
}