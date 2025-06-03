namespace SOS_Natureza.Infrastructure.DTOs
{
    /// <summary>
    /// DTO para atualização de uma denúncia existente.
    /// </summary>
    public class UpdateDenunciaDto
    {
        /// <summary>
        /// Nova categoria da denúncia.
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Nova descrição da denúncia.
        /// </summary>
        public string? Descricao { get; set; }
    }
}
