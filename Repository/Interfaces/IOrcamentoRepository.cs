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
            string usuarioId
        );

        Task<IEnumerable<Orcamento>> GetByUsuarioMesAnoAsync(
            string usuarioId,
            int mes,
            int ano
        );

        Task UpdateAsync(Orcamento orcamento);

        Task<bool> DeleteAsync(int id, string usuarioId);
        Task<bool> ExistsAsync(int categoriaId, int mes, int ano, string usuarioId);
        
    }
}
