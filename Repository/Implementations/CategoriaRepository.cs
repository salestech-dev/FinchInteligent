using finchInteligent.Data;
using finchInteligent.Enums;
using finchInteligent.Models;
using finchInteligent.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria> CreateAsync(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria?> GetByIdAsync(int id, int usuarioId)
    {
        return await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == usuarioId);
    }

    public async Task<IEnumerable<Categoria>> GetAllByUsuarioAsync(int usuarioId)
    {
        return await _context.Categorias
            .Where(c => c.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Categoria>> GetByTipoAsync(TipoDeCategoria tipo, int usuarioId)
    {
        return await _context.Categorias
            .Where(c => c.TipoDeCategoria == tipo && c.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id, int usuarioId)
    {
        var categoria = await GetByIdAsync(id, usuarioId);
        if (categoria == null) return false;

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<bool> ExistsAsync(string nome, TipoDeCategoria tipo, int usuarioId)
{
    return _context.Categorias.AnyAsync(c =>
        c.Nome == nome &&
        c.TipoDeCategoria == tipo &&
        c.UsuarioId == usuarioId
    );
}
}
