using finchInteligent.Models;

namespace finchInteligent.Repository.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<Transacao> CreateAsync(Transacao transacao);

        Task<Transacao?> GetByIdAsync(int id, string usuarioId);

        Task<IEnumerable<Transacao>> GetAllByUsuarioAsync(string usuarioId);

        Task<IEnumerable<Transacao>> GetByContaAsync(int contaId, string usuarioId);

        Task<IEnumerable<Transacao>> GetByCategoriaAsync(int categoriaId, string usuarioId);

        Task<IEnumerable<Transacao>> GetByPeriodoAsync(string usuarioId, int mes, int ano);

        Task<bool> DeleteAsync(int id, string usuarioId);
        Task<decimal> GetTotalSaidasByCategoriaMesAsync(
                    int categoriaId,
                    int mes,
                    int ano,
                    string usuarioId
        );
    }
}
