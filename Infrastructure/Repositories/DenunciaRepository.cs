using Microsoft.EntityFrameworkCore;
using SOS_Natureza.Domain.Entities;
using SOS_natureza.Infrastructure.Context;

namespace SOS_natureza.Infrastructure.Repositories
{
    public class DenunciaRepository : Repository<Denuncia>, IDenunciaRepository
    {
        public DenunciaRepository(AppDbContext context) : base(context) { }

        public async Task<Denuncia?> GetByIdAsync(int id)
        {
            return await _context.Denuncias
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Denuncia>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Denuncias
                .Where(d => d.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Denuncia>> FindByCategoriaAsync(string categoria)
        {
            return await _context.Denuncias
                .Where(d => d.Categoria.ToLower() == categoria.ToLower())
                .ToListAsync();
        }
    }
}
