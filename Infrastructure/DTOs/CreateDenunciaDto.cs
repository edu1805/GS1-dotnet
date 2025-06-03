namespace SOS_Natureza.Infrastructure.DTOs
{
    /// <summary>
    /// DTO para criação de uma nova denúncia.
    /// </summary>
    public class CreateDenunciaDto
    {
        /// <summary>
        /// ID do usuário que realizou a denúncia.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Descrição da denúncia.
        /// </summary>
        public string? Descricao { get; set; }

        /// <summary>
        /// Categoria da denúncia (ex: lixo, desmatamento, queimada, poluição, alagamento).
        /// </summary>
        public string Categoria { get; set; }
    }
}
