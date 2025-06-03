namespace SOS_Natureza.Infrastructure.DTOs
{
    /// <summary>
    /// DTO para criação de um novo usuário.
    /// </summary>
    public class CreateUsuarioDto
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
        /// Senha do usuário (mínimo 7 caracteres).
        /// </summary>
        public string Password { get; set; }
    }
}
