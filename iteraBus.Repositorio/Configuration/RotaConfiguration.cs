using iteraBus.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iteraBus.Repositorio.Configuration
{
    public class RotaConfiguration : IEntityTypeConfiguration<Rota>
    {
        public void Configure(EntityTypeBuilder<Rota> builder)
        {
            builder.ToTable("Rotas").HasKey(r => r.Id);

            builder.Property(nameof(Rota.Id)).HasColumnName("RotaId").IsRequired(true);
            builder.Property(nameof(Rota.Nome)).HasColumnName("Nome").IsRequired(true);

            builder.HasMany(r => r.Onibus)
            .WithOne(o => o.Rota)
            .HasForeignKey(o => o.RotaId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.PontosDeOnibus)
            .WithOne(p => p.Rota)
            .HasForeignKey(p => p.RotaId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.UsuariosQueFavoritaram)
            .WithMany(u => u.RotasFavoritas)
            .UsingEntity(j => j.ToTable("UsuarioRotasFavoritas"));
        }
    }
}