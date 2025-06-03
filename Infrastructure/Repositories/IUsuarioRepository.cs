using SOS_Natureza.Domain.Entities;

namespace SOS_natureza.Infrastructure.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> GetByEmailAsync(string email);
        Task<Usuario?> GetByIdAsync(int id);
        Task<IEnumerable<Usuario>> FindByNameAsync(string name);
    }
}
