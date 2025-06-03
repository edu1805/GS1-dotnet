namespace SOS_Natureza.Infrastructure.DTOs
{
    /// <summary>
    /// DTO de leitura para exibição de dados da denúncia.
    /// </summary>
    public class DenunciaDTO
    {
        /// <summary>
        /// ID da denúncia.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID do usuário que fez a denúncia.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Descrição da denúncia
        /// </summary>
        public string? Descricao { get; set; }

        /// <summary>
        /// Categoria da denúncia.
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Data da denúncia.
        /// </summary>
        public DateTime Data { get; set; }
    }
}
