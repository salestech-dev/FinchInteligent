using finchInteligent.Data;
using finchInteligent.Models;
using finchInteligent.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace finchInteligent.Repository.Implementations
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly AppDbContext _context;

        public TransacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transacao> CreateAsync(Transacao transacao)
        {
            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }

        public async Task<Transacao?> GetByIdAsync(int id, int usuarioId)
        {
            return await _context.Transacoes
                .Include(t => t.Conta)
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(t => t.Id == id && t.Usuario.Id == usuarioId);
        }

        public async Task<IEnumerable<Transacao>> GetAllByUsuarioAsync(int usuarioId)
        {
            return await _context.Transacoes
                .Where(t => t.Usuario.Id == usuarioId)
                .OrderByDescending(t => t.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transacao>> GetByContaAsync(int contaId, int usuarioId)
        {
            return await _context.Transacoes
                .Where(t => t.Conta.Id == contaId && t.Usuario.Id == usuarioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transacao>> GetByCategoriaAsync(int categoriaId, int usuarioId)
        {
            return await _context.Transacoes
                .Where(t => t.Categoria.Id == categoriaId && t.Usuario.Id == usuarioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transacao>> GetByPeriodoAsync(int usuarioId, int mes, int ano)
        {
            return await _context.Transacoes
                .Where(t => t.Usuario.Id == usuarioId &&
                            t.Data.Month == mes &&
                            t.Data.Year == ano)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id, int usuarioId)
        {
            var transacao = await GetByIdAsync(id, usuarioId);
            if (transacao == null) return false;

            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
