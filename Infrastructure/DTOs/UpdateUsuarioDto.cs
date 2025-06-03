namespace SOS_Natureza.Infrastructure.DTOs
{
    /// <summary>
    /// DTO para atualização de dados de um usuário.
    /// </summary>
    public class UpdateUsuarioDto
    {
        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail do usuário.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Nova senha (opcional, mínimo 7 caracteres se fornecida).
        /// </summary>
        public string? Password { get; set; }
    }
}
