using finchInteligent.Enums;
using finchInteligent.Models;

public interface ICategoriaRepository
{
    Task<Categoria> CreateAsync(Categoria categoria);

    Task<Categoria?> GetByIdAsync(int id, int usuarioId);

    Task<IEnumerable<Categoria>> GetAllByUsuarioAsync(int usuarioId);

    Task<IEnumerable<Categoria>> GetByTipoAsync(TipoDeCategoria tipo, int usuarioId);
    Task<bool> ExistsAsync(string nome, TipoDeCategoria tipo, int usuarioId);
    Task UpdateAsync(Categoria categoria);

    Task<bool> DeleteAsync(int id, int usuarioId);
}
