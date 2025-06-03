using Microsoft.EntityFrameworkCore;
using SOS_Natureza.Domain.Entities;
using SOS_natureza.Infrastructure.Context;

namespace SOS_natureza.Infrastructure.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context) { }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios
                .Include(u => u.Denuncias)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Denuncias)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Usuario>> FindByNameAsync(string name)
        {
            return await _context.Usuarios
                .Where(u => u.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }
    }
}
