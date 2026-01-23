using finchInteligent.Data;
using finchInteligent.Models;
using finchInteligent.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ContaRepository : IContaRepository
{
    private readonly AppDbContext _context;

    public ContaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Conta> CreateAsync(Conta conta)
    {
        await _context.Contas.AddAsync(conta);
        await _context.SaveChangesAsync();
        return conta;
    }

    public async Task<Conta?> GetByIdAsync(int id, int usuarioId)
    {
        return await _context.Contas
            .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == usuarioId);
    }

    public async Task<IEnumerable<Conta>> GetAllByUsuarioAsync(int usuarioId)
    {
        return await _context.Contas
            .Where(c => c.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Conta conta)
    {
        _context.Contas.Update(conta);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id, int usuarioId)
    {
        var conta = await GetByIdAsync(id, usuarioId);
        if (conta == null) return false;

        _context.Contas.Remove(conta);
        await _context.SaveChangesAsync();
        return true;
    }
}
