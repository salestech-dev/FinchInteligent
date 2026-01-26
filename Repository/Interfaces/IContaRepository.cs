using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finchInteligent.Models;


namespace finchInteligent.Repository.Interfaces
{
    public interface IContaRepository
    {
        Task<Conta> CreateAsync(Conta conta);

        Task<Conta?> GetByIdAsync(int id, string usuarioId);

        Task<IEnumerable<Conta>> GetAllByUsuarioAsync(string usuarioId);

        Task UpdateAsync(Conta conta);

        Task<bool> DeleteAsync(int id, string usuarioId);
        Task<Conta?> GetByIdAndUsuarioAsync(int contaId, string usuarioId);
    }
}