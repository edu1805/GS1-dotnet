using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOS_Natureza.Domain.Entities;

namespace SOS_natureza.Infrastructure.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("T_SOS_USUARIOS");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("ID")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Name)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("SENHA")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(u => u.Denuncias)
                   .WithOne()
                   .HasForeignKey(d => d.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
