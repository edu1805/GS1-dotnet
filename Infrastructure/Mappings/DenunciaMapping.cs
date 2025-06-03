using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOS_Natureza.Domain.Entities;

namespace SOS_Natureza.Infrastructure.Mappings
{
    public class DenunciaMapping : IEntityTypeConfiguration<Denuncia>
    {
        public void Configure(EntityTypeBuilder<Denuncia> builder)
        {
            builder.ToTable("T_SOS_DENUNCIAS");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .HasColumnName("ID")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(d => d.Categoria)
                .HasColumnName("CATEGORIA")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(d => d.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(150);

            builder.Property(d => d.Data)
                .HasColumnName("DATA")
                .IsRequired();

            builder.Property(d => d.UsuarioId)
                .HasColumnName("USUARIO_ID")
                .IsRequired();
        }
    }
}
