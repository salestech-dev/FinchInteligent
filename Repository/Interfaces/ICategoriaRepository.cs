using finchInteligent.Enums;
using finchInteligent.Models;

public interface ICategoriaRepository
{
    Task<Categoria> CreateAsync(Categoria categoria);

    Task<Categoria?> GetByIdAsync(int id, string usuarioId);

    Task<IEnumerable<Categoria>> GetAllByUsuarioAsync(string usuarioId);

    Task<IEnumerable<Categoria>> GetByTipoAsync(TipoDeCategoria tipo, string usuarioId);
    Task<bool> ExistsAsync(string nome, TipoDeCategoria tipo, string usuarioId);
    Task UpdateAsync(Categoria categoria);

    Task<bool> DeleteAsync(int id, string usuarioId);
    Task<Categoria?> GetByIdAndUsuarioAsync(int categoriaId, string usuarioId);
}
