using SOS_Natureza.Domain.Entities;

namespace SOS_natureza.Infrastructure.Repositories
{
    public interface IDenunciaRepository : IRepository<Denuncia>
    {
        Task<Denuncia?> GetByIdAsync(int id);
        Task<IEnumerable<Denuncia>> GetByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<Denuncia>> FindByCategoriaAsync(string categoria);
    }
}
