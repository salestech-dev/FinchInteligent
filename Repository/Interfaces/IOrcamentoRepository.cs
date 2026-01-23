using finchInteligent.Models;

namespace finchInteligent.Repository.Interfaces
{
    public interface IOrcamentoRepository
    {
        Task<Orcamento> CreateAsync(Orcamento orcamento);

        Task<Orcamento?> GetByCategoriaMesAnoAsync(
            int categoriaId,
            int mes,
            int ano,
            int usuarioId
        );

        Task<IEnumerable<Orcamento>> GetByUsuarioMesAnoAsync(
            int usuarioId,
            int mes,
            int ano
        );

        Task UpdateAsync(Orcamento orcamento);

        Task<bool> DeleteAsync(int id, int usuarioId);
    }
}
