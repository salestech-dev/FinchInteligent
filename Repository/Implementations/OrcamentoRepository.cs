using finchInteligent.Data;
using finchInteligent.Models;
using finchInteligent.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace finchInteligent.Repository.Implementations
{
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private readonly AppDbContext _context;

        public OrcamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Orcamento> CreateAsync(Orcamento orcamento)
        {
            await _context.Orcamentos.AddAsync(orcamento);
            await _context.SaveChangesAsync();
            return orcamento;
        }

        public async Task<Orcamento?> GetByCategoriaMesAnoAsync(
            int categoriaId,
            int mes,
            int ano,
            int usuarioId)
        {
            return await _context.Orcamentos
                .FirstOrDefaultAsync(o =>
                    o.CategoriaId == categoriaId &&
                    o.Mes == mes &&
                    o.Ano == ano &&
                    o.UsuarioId == usuarioId
                );
        }

        public async Task<IEnumerable<Orcamento>> GetByUsuarioMesAnoAsync(
            int usuarioId,
            int mes,
            int ano)
        {
            return await _context.Orcamentos
                .Where(o =>
                    o.UsuarioId == usuarioId &&
                    o.Mes == mes &&
                    o.Ano == ano
                )
                .ToListAsync();
        }

        public async Task UpdateAsync(Orcamento orcamento)
        {
            _context.Orcamentos.Update(orcamento);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id, int usuarioId)
        {
            var orcamento = await _context.Orcamentos
                .FirstOrDefaultAsync(o => o.Id == id && o.UsuarioId == usuarioId);

            if (orcamento == null) return false;

            _context.Orcamentos.Remove(orcamento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
