using finchInteligent.Models;

namespace finchInteligent.Repository.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<Transacao> CreateAsync(Transacao transacao);

        Task<Transacao?> GetByIdAsync(int id, int usuarioId);

        Task<IEnumerable<Transacao>> GetAllByUsuarioAsync(int usuarioId);

        Task<IEnumerable<Transacao>> GetByContaAsync(int contaId, int usuarioId);

        Task<IEnumerable<Transacao>> GetByCategoriaAsync(int categoriaId, int usuarioId);

        Task<IEnumerable<Transacao>> GetByPeriodoAsync(int usuarioId, int mes, int ano);

        Task<bool> DeleteAsync(int id, int usuarioId);
    }
}
