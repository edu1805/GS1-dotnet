using System.ComponentModel.DataAnnotations;

namespace SOS_Natureza.Domain.Entities
{
    public class Denuncia
    {
        private static readonly string[] categorias = { "lixo", "desmatamento", "queimada", "poluição", "alagamento" };

        public int Id { get; private set; }
        public int UsuarioId { get; private set; }

        public string Descricao { get; private set; }

        [Required]
        [MaxLength(50)]
        public string Categoria { get; private set; }

        public DateTime Data { get; private set; } = DateTime.UtcNow;

        public Denuncia() { }

        public Denuncia(int usuarioId, string categoria, string descricao)
        {
            UsuarioId = usuarioId;
            AtualizarCategoria(categoria);
            this.Descricao = descricao;
        }

        public void AtualizarCategoria(string novaCategoria)
        {
            if (string.IsNullOrWhiteSpace(novaCategoria))
                throw new ArgumentException("A categoria não pode ser nula ou vazia.");

            var categoriaFormatada = novaCategoria.Trim().ToLower();

            if (!categorias.Contains(categoriaFormatada))
                throw new ArgumentException(
                    $"Categoria inválida: '{novaCategoria}'. As opções válidas são: {string.Join(", ", categorias)}."
                );

            Categoria = categoriaFormatada;
        }

        public void AtualizarDescricao(string novaDescricao)
        {
            if (string.IsNullOrWhiteSpace(novaDescricao))
                throw new ArgumentException("A descrição não pode ser nula ou vazia.");

            Descricao = novaDescricao.Trim();
        }

    }
}