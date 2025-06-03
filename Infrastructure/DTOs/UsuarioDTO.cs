namespace SOS_Natureza.Infrastructure.DTOs
{
    /// <summary>
    /// DTO de leitura para exibição de dados do usuário.
    /// </summary>
    public class UsuarioDTO
    {
        /// <summary>
        /// ID do usuário.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail do usuário.
        /// </summary>
        public string Email { get; set; }
    }
}
