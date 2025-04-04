using iteraBus.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iteraBus.Repositorio.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario").HasKey(x => x.Id);

            builder.Property(nameof(Usuario.Id)).HasColumnName("UsuarioId");
            builder.Property(nameof(Usuario.Nome)).HasColumnName("Nome").IsRequired(true);
            builder.Property(nameof(Usuario.Email)).HasColumnName("Email").IsRequired(true);
            builder.Property(nameof(Usuario.Senha)).HasColumnName("Senha").IsRequired(true);
            builder.Property(nameof(Usuario.Ativo)).HasColumnName("Ativo").IsRequired(true);
            builder.Property(nameof(Usuario.TipoUsuarioId)).HasColumnName("TipoUsuarioId").IsRequired(true);
            builder.Property(nameof(Usuario.TokenExpiracao)).HasColumnName("TokenExpiracao");
            builder.Property(nameof(Usuario.TokenRecuperacao)).HasColumnName("TokenRecuperacao");

            builder
                .HasMany(u => u.RotasFavoritas)
                .WithMany(r => r.UsuariosQueFavoritaram)
                .UsingEntity(j => 
                    j.ToTable("UsuarioRotasFavoritas"));
        }
    }
}